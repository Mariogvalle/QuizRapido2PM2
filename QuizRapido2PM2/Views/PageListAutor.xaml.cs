using QuizRapido2PM2.Models;
using System.Collections.ObjectModel;

namespace QuizRapido2PM2.Views;

public partial class PageListAutor : ContentPage
{
    private ObservableCollection<Autores> _autor;
    public PageListAutor()
	{
		InitializeComponent();
	}

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Views.PageInit page = new PageInit(0);
        Navigation.PushAsync(page);
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        listautor.ItemsSource = await App.DataBase.GetListAutores();
    }

    private void OnAutorSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.Autores selectedAutor)
        {
            //var initPage = new PageInit(selectedPerson.Id);
            Views.PageInit page = new PageInit(selectedAutor.Id);
            Navigation.PushAsync(page);
        }

    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue.ToLower();
        var filteredAutor = _autor.Where(p =>
            p.Nombres.ToLower().Contains(searchText) ||
            p.Apellidos.ToLower().Contains(searchText)
        ).ToList();
        listautor.ItemsSource = filteredAutor;
    }


}