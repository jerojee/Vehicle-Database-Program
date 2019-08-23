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
    /// Interaction logic for DeleteByIdDialogueBox.xaml
    /// </summary>
    public partial class DeleteByIdDialogueBox : Window
    {
        public DeleteByIdDialogueBox()
        {
            InitializeComponent();
            //clear all text boxes for user input
            VehicleIdBox.Text = null;
        }

        private void Handle_OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get vehicle ID
                int VehicleId = Convert.ToInt32(VehicleIdBox.Text);
   
                //Pass to SQLite Vehicle database access
                SqliteDataAccess.DeleteVehiclesById(VehicleId);

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
