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
using System.Windows.Shapes;

namespace VehicleService
{
    /// <summary>
    /// Interaction logic for FilterVehiclesDialougeBox.xaml
    /// </summary>
    public partial class FilterVehiclesDialougeBox : Window
    {
        public FilterVehiclesDialougeBox()
        {
            InitializeComponent();
            IdBox.Text = null; //Clear all text boxes
            YearBox.Text = null;
            MakeBox.Text = null;
            ModelBox.Text = null;
        }

        private void Handle_GetVehiclesButton_Click(object sender, RoutedEventArgs e)
        {
            //((MainWindow)Application.Current.MainWindow).VehicleListBox.ItemsSource = SqliteDataAccess.GetVehiclesById(VehicleId);
            try
            {
                //create new vehicle
                Vehicle vehicle = new Vehicle
                {
                    Make = MakeBox.Text.ToUpper(),
                    Model = ModelBox.Text.ToUpper()
                };

                //We cannot parse an empty string to an int, so we must check these cases
                //set Vehicle Id and year to default 0 if empty
                if (string.IsNullOrEmpty(IdBox.Text) && string.IsNullOrEmpty(YearBox.Text))
                {
                    vehicle.Id = 0;
                    vehicle.Year = 0;
                }

                //check if year is empty
                if (!string.IsNullOrEmpty(IdBox.Text) && string.IsNullOrEmpty(YearBox.Text))
                {
                    vehicle.Id = Convert.ToInt32(IdBox.Text);
                    vehicle.Year = 0;

                }
                //check if Id is empty
                if (string.IsNullOrEmpty(IdBox.Text) && !string.IsNullOrEmpty(YearBox.Text))
                {
                    vehicle.Id = 0;
                    vehicle.Year = Convert.ToInt32(YearBox.Text);                 
                }

                //check uf year and id are empty
                if (!string.IsNullOrEmpty(IdBox.Text) && !string.IsNullOrEmpty(YearBox.Text))
                {
                    vehicle.Id = Convert.ToInt32(IdBox.Text);
                    vehicle.Year = Convert.ToInt32(YearBox.Text);
                }
                                  
                //Pass instance of Main Window to update ListBox
                //Pass to SQLite Vehicle database access
                ((MainWindow)Application.Current.MainWindow).VehicleListBox.ItemsSource = SqliteDataAccess.FilterVehicles(vehicle);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Occured");
            }
        }

        private void Handle_CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
