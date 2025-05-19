using MauiAppMinhasCompras.Models; // Importa o namespace que cont�m o modelo "Produto"

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    public EditarProduto()
    {
        InitializeComponent(); // Inicializa os componentes da interface da p�gina
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obt�m o objeto "Produto" atualmente vinculado ao BindingContext da p�gina
            Produto produto_anexado = BindingContext as Produto;

            // Cria um novo objeto "Produto" com os dados atualizados da interface
            Produto p = new Produto
            {
                Id = produto_anexado.Id, // Mant�m o mesmo ID do produto original
                Descricao = txt_descricao.Text, // Atualiza a descri��o
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte a quantidade para double
                Preco = Convert.ToDouble(txt_preco.Text) // Converte o pre�o para double
            };

            // Atualiza o produto no banco de dados
            await App.Db.Update(p);

            // Exibe um alerta informando que o registro foi atualizado com sucesso
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

            // Retorna para a p�gina anterior
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Em caso de erro, exibe um alerta com a mensagem da exce��o
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
