using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
        // Inicializa os componentes da interface gráfica da página.
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Validação para garantir que os campos não estejam vazios.
            if (string.IsNullOrWhiteSpace(txt_descricao.Text) ||
                string.IsNullOrWhiteSpace(txt_quantidade.Text) ||
                string.IsNullOrWhiteSpace(txt_preco.Text))
            {
                await DisplayAlert("Erro", "Todos os campos devem ser preenchidos.", "OK");
                return;
            }

            // Conversão segura para evitar erros caso a entrada seja inválida.
            if (!double.TryParse(txt_quantidade.Text, out double quantidade))
            {
                await DisplayAlert("Erro", "Quantidade inválida. Digite um número válido.", "OK");
                return;
            }

            if (!double.TryParse(txt_preco.Text, out double preco))
            {
                await DisplayAlert("Erro", "Preço inválido. Digite um número válido.", "OK");
                return;
            }

            // Criação de um novo objeto Produto com os valores inseridos.
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = quantidade,
                Preco = preco
            };

            // Insere o novo produto no banco de dados.
            await App.Db.Insert(p);

            // Exibe um alerta informando que o produto foi salvo com sucesso.
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

            // Retorna à página anterior após a inserção.
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Captura e exibe qualquer erro inesperado.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
