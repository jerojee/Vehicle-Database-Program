using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VehicleService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Vehicle> ListOfVehicles = new List<Vehicle>(); //To hold returned list of vehicles from database query

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadVehicleListBox(List<Vehicle> ListOfVehicles)
        {
            //Set List Box UI to display current content of vehicle list
            VehicleListBox.ItemsSource = ListOfVehicles;
        }

        //GET VEHICLES
        private void Handle_GetAllVehiclesButton_Click(object sender, RoutedEventArgs e)
        {
            //SQLite vehicle database access
            ListOfVehicles = SqliteDataAccess.GetAllVehicles();

            //Display List
            LoadVehicleListBox(ListOfVehicles);
        }
       
        //GET VEHICLES BY ID
        private void Handle_GetAllVehiclesByIdButton_Click(object sender, RoutedEventArgs e)
        {
            //Create and show enter by Id window
            ByIdDialogueBox IdDialogueBox = new ByIdDialogueBox();
            IdDialogueBox.Show();
        }

        //CREATE VEHICLE
        private void Handle_CreateVehiclesButton_Click(object sender, RoutedEventArgs e)
        {           
            //Create and show create vehicle window
            CreateVehicleDialogueBox CreateVehicleWindow = new CreateVehicleDialogueBox();
            CreateVehicleWindow.Show();                   
        }

        //UPDATE VEHICLE
        private void Handle_UpdateVehiclesButton_Click(object sender, RoutedEventArgs e)
        {
            //Create and show update vehicle window
            UpdateVehicleDialogueBox UpdateVehicleWindow = new UpdateVehicleDialogueBox();
            UpdateVehicleWindow.Show();
        }

        //DELETE VEHICLE BY ID
        private void Handle_DeleteVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            //Create and show delete by ID vehicle window
            DeleteByIdDialogueBox DeleteVehicleByIdWindow = new DeleteByIdDialogueBox();
            DeleteVehicleByIdWindow.Show();
        }

        private void Handle_FilterVehiclesButton_Click(object sender, RoutedEventArgs e)
        {
            FilterVehiclesDialougeBox FilterVehiclesWindow = new FilterVehiclesDialougeBox();
            FilterVehiclesWindow.Show();
        }
    }
}
