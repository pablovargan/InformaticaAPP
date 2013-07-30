using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Text.RegularExpressions;
using InformaticaAPP.ViewModels;


namespace InformaticaAPP
{
    public partial class DetallesNotas : PhoneApplicationPage
    {
        public DetallesNotas()
        {
            InitializeComponent();
        }
        // Propiedades
        public DlsiVM PostActual { get; set; }

        // Cuando llegamos a la página, establecemos el elemento seleccionado como contexto de datos
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                this.PostActual = App.ViewModel.Items[index];
                this.DataContext = this.PostActual;
            }
        }

        //Evento para iniciar el navegador
        private void ContenidoHTML_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Navegador.Navigate(new Uri(Constantes.URL_Web, UriKind.Absolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected Exception", MessageBoxButton.OK);
            }
        }

        //Evento para compartir en redes sociales la nota disponible
        private void Compartir_Click(object sender, EventArgs e)
        {
            ShareMediaTask shareMedia = new ShareMediaTask();
            //shareMedia.FilePath = this.PostActual();
            shareMedia.Show();
        }
    }
}