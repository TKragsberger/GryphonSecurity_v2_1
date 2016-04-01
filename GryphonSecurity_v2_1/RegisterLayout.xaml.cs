using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using GryphonSecurity_v2_1.Domain.Entity;
using GryphonSecurity_v2_1.Resources;

namespace GryphonSecurity_v2_1
{
    public partial class RegisterLayout : PhoneApplicationPage
    {
        Controller controller = Controller.Instance;
        User user;
        public RegisterLayout()
        {
            InitializeComponent();
        }

        private void RegistrerBrugerButton_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    User localUser = new User(user.Firstname, user.Lastname, user.Address, user.Phonenumber, user.Username, user.Password);
                    if (controller.createUser(localUser))
                    {
                        MessageBox.Show(AppResources.UserCreated);
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show(AppResources.UserNotCreated);
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show(AppResources.UserRegistrationError);
                }
            
        }

        private void SearchForUserButton_Click(object sender, RoutedEventArgs e)
        {
            long id = Convert.ToInt64(textBoxFirstname.Text);
            user = controller.getUser(id);

        }
    }
}