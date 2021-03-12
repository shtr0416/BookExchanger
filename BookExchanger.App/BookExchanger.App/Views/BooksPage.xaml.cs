using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookExchanger.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage
    {
        public List<Book> Books { get; set; }

        public ICommand FavoriteCommand { private set; get; }

        public BooksPage()
        {
            InitializeComponent();
            this.InitData();
            FavoriteCommand = InitCommand();

            BindingContext = this;
        }

        private ICommand InitCommand()
        {
            return new Command(execute: async () =>
            {
                Console.WriteLine("------------------");
                await DisplayAlert("Fav", "Fav by command", "OK");
            });
        }

        private void InitData()
        {
            Books = new List<Book>();
            Books.Add(new Book("AAA01", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA02", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA03", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA04", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA05", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA06", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA07", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA08", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA09", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA10", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA11", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA12", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA13", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA14", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA15", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA16", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA17", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA18", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA19", "AAA", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
            Books.Add(new Book("AAA20", "BBB", "https://ss3.bdstatic.com/70cFv8Sh_Q1YnxGkpoWK1HF6hhy/it/u=2917531912,3856498833&fm=26&gp=0.jpg"));
        }
    }

    public class Book
    {
        public Book(string name, string level, string uri)
        {
            this.Name = name;
            this.Level = level;
            this.Uri = uri;
        }

        public string Name { get; set; }
        public string Uri { get; set; }
        public string Level { get; set; }
    }
}