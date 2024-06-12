using QuizRapido2PM2.Models;
using QuizRapido2PM2.Services;
namespace QuizRapido2PM2.Views;

public partial class PageInit : ContentPage
{
    FileResult photo;
    private string _photoBase64;
    private Models.Autores _selectedAutor;
    private int? _autorId;
    private Autores _autor;
    private readonly CountryService _countryService;
    String Base64 = String.Empty;

    public PageInit(int? autorId)
    {
        InitializeComponent();
        _autorId = autorId;
        _countryService = new CountryService();
        LoadCountries();
        LoadAutor();
    }

    private async void LoadAutor()
    {
        if (_autorId.HasValue)
        {
            _autor = await App.DataBase.GetAutor(_autorId.Value);
            if (_autor != null)
            {
                Nombres.Text = _autor.Nombres;
                Apellidos.Text = _autor.Apellidos;
                Telefono.Text = _autor.Telefono;
                FechaNac.Date = _autor.FechaNac;
//                CountryPicker..Text = _autor.Nacionalidad;
                _photoBase64 = _autor.Foto;
                FotoImage.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(_photoBase64)));
                ((Button)FindByName("btnActualizar")).IsVisible = true;
                ((Button)FindByName("btnEliminar")).IsVisible = true;
                ((Button)FindByName("btnAceptar")).IsVisible = false;
            }
        }
    }


    private async void LoadCountries()
    {

    }

    public String GetImage64()
    {
        String Base64 = String.Empty;

        if (photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();

                Base64 = Convert.ToBase64String(data);
                return Base64;
            }
        }

        return Base64;
    }


    public byte[] GetImageArray()
    {
        byte[] data = new Byte[] { };

        if (photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                data = ms.ToArray();

                return data;
            }
        }

        return data;
    }

    private async void btnaceptar_Clicked(object sender, EventArgs e)
    {

        var autor = new Models.Autores
        {
            Nombres = Nombres.Text,
            Apellidos = Apellidos.Text,
            FechaNac = FechaNac.Date,
            Telefono = Telefono.Text,
            //Nacionalidad = Nacionalidad.Text,
            Nacionalidad = nationalityPicker.SelectedItem?.ToString(),
            Foto = GetImage64()
        };

        if (await App.DataBase.StoreAutor(autor) > 0)
        {
            await DisplayAlert("Aviso", "Registro ingresado con exito!!", "OK");
            clearView();
        }
    }

    private void OnUpdateClicked(object sender, EventArgs e)
    {
        if (_autor != null)
        {
            _autor.Nombres = Nombres.Text;
            _autor.Apellidos = Apellidos.Text;
            _autor.Telefono = Telefono.Text;
            _autor.FechaNac = FechaNac.Date;
            _autor.Nacionalidad = nationalityPicker.SelectedItem?.ToString();
            _autor.Foto = _photoBase64;

            App.DataBase.StoreAutor(_autor);
            clearView();
        }
        else
        {
            DisplayAlert("Error", "No es posible actualizar", "OK");
        }
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_autor != null)
        {
            App.DataBase.DeleteAutor(_autor);
            clearView();
        }
        else
        {
            DisplayAlert("Error", "No es posible eliminar", "OK");
        }
    }

    private async void btnfoto_Clicked(object sender, EventArgs e)
    {
        try
        {
            photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                string Localizacion = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream imagenlocal = File.OpenWrite(Localizacion);

                FotoImage.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);

                await sourceStream.CopyToAsync(imagenlocal);

            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void clearView()
    {
        Nombres.Text = string.Empty;
        Apellidos.Text = string.Empty;
        Telefono.Text = string.Empty;
        FechaNac.Date = DateTime.Today;
        nationalityPicker.SelectedIndex = -1;
        //        FotoImage.ext = string.Empty;

    }

}