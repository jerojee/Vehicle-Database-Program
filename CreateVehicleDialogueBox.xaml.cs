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
    /// Interaction logic for CreateVehicleDialogueBox.xaml
    /// </summary>
    public partial class CreateVehicleDialogueBox : Window
    {
        public CreateVehicleDialogueBox()
        {
            InitializeComponent();

            IdBox.Text = null; //Clear all text boxes
            YearBox.Text = null;
            MakeBox.Text = null;
            ModelBox.Text = null;
        }

        private void Handle_OkButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //Create new vehicle
                Vehicle vehicle = new Vehicle
                {
                    Id = Convert.ToInt32(IdBox.Text),
                    Year = Convert.ToInt32(YearBox.Text),
                    Make = MakeBox.Text.ToUpper(),
                    Model = ModelBox.Text.ToUpper()
                };

                //Pass to SQLite Vehicle database access
                SqliteDataAccess.CreateVehicles(vehicle);

                this.Close();
            }
            catch(Exception ex)
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
