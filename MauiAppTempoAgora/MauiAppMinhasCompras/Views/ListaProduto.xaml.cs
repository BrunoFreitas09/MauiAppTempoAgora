using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;


namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    // Coleção observável que armazena a lista de produtos exibidos na tela.

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
        // Define a origem de dados da ListView como a coleção observável "lista".
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear(); // Limpa a lista antes de carregar novos dados.

            List<Produto> tmp = await App.Db.GetAll();
            // Busca todos os produtos armazenados no banco de dados.

            tmp.ForEach(i => lista.Add(i)); // Adiciona os produtos recuperados à coleção observável.
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK"); // Exibe uma mensagem de erro caso ocorra uma exceção.
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
            // Navega para a página de cadastro de um novo produto.
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK"); // Exibe um alerta em caso de erro.
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue; // Obtém o texto digitado pelo usuário.

            lista.Clear(); // Limpa a lista antes de atualizar os dados filtrados.

            List<Produto> tmp = await App.Db.Search(q);
            // Busca produtos filtrados com base no texto digitado.

            tmp.ForEach(i => lista.Add(i)); // Adiciona os produtos filtrados à lista.
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK"); // Exibe uma mensagem de erro caso ocorra uma exceção.
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);
        // Calcula a soma do valor total de todos os produtos da lista.

        string msg = $"O total é {soma:C}";
        // Formata a mensagem para exibição com formato monetário.

        DisplayAlert("Total dos Produtos", msg, "OK");
        // Exibe um alerta mostrando o total dos produtos.
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecinado = sender as MenuItem;
            // Obtém o item do menu de contexto que foi clicado.

            Produto p = selecinado.BindingContext as Produto;
            // Obtém o objeto "Produto" associado ao item selecionado.

            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");
            // Exibe um alerta de confirmação antes de excluir o produto.

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                // Remove o produto do banco de dados.

                lista.Remove(p);
                // Remove o produto da lista exibida na interface.
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
