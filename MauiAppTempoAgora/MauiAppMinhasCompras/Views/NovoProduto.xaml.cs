using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
        // Inicializa os componentes da interface gr�fica da p�gina.
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Valida��o para garantir que os campos n�o estejam vazios.
            if (string.IsNullOrWhiteSpace(txt_descricao.Text) ||
                string.IsNullOrWhiteSpace(txt_quantidade.Text) ||
                string.IsNullOrWhiteSpace(txt_preco.Text))
            {
                await DisplayAlert("Erro", "Todos os campos devem ser preenchidos.", "OK");
                return;
            }

            // Convers�o segura para evitar erros caso a entrada seja inv�lida.
            if (!double.TryParse(txt_quantidade.Text, out double quantidade))
            {
                await DisplayAlert("Erro", "Quantidade inv�lida. Digite um n�mero v�lido.", "OK");
                return;
            }

            if (!double.TryParse(txt_preco.Text, out double preco))
            {
                await DisplayAlert("Erro", "Pre�o inv�lido. Digite um n�mero v�lido.", "OK");
                return;
            }

            // Cria��o de um novo objeto Produto com os valores inseridos.
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

            // Retorna � p�gina anterior ap�s a inser��o.
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Captura e exibe qualquer erro inesperado.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
