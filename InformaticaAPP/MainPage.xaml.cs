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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace InformaticaAPP
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Establecer el contexto de datos del control ListBox control en los datos de ejemplo
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            App.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(delegate()
            {
                DlsiListBox.ItemsSource = App.ViewModel.Items;
            });
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (DlsiListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetallesNotas.xaml?selectedItem=" + DlsiListBox.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            DlsiListBox.SelectedIndex = -1;
        }

        //Evento que carga datos para los elementos ViewModel
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                //Muestro el progressBar de cargando notas
                App.ViewModel.LoadData();
                BarraProgreso.IsVisible = false;
            }
        }
        //Evento para actualizar el rss del dlsi
        private void Actualizar_Click(object sender, EventArgs e)
        {
            App.ViewModel.LoadData();
        }
    }
}