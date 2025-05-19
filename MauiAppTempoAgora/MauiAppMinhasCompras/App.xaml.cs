using MauiAppMinhasCompras.Helpers; // Importa o namespace que contém a classe de auxílio para o banco de dados SQLite

namespace MauiAppMinhasCompras
{
    public partial class App : Application // Classe principal da aplicação, derivada de Application
    {
        static SQLiteDatabaseHelper _db; // Declara uma variável estática para armazenar a instância do banco de dados SQLite

        // Propriedade estática que retorna a instância do banco de dados SQLite
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                // Verifica se a instância do banco de dados já foi criada
                if (_db == null)
                {
                    // Define o caminho do banco de dados dentro do diretório de dados locais da aplicação
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), // Obtém o diretório local para armazenamento de dados
                        "banco_sqlite_compras.db3"); // Nome do arquivo do banco de dados

                    // Cria uma nova instância do banco de dados passando o caminho do arquivo
                    _db = new SQLiteDatabaseHelper(path);
                }

                return _db; // Retorna a instância do banco de dados
            }
        }

        // Construtor da classe App, responsável pela inicialização da aplicação
        public App()
        {
            InitializeComponent(); // Inicializa os componentes da aplicação, necessário para MAUI

            // Define a página principal da aplicação. A página inicial é uma NavigationPage contendo a lista de produtos.
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
