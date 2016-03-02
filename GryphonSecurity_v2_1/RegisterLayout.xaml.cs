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
        public RegisterLayout()
        {
            InitializeComponent();
        }

        private void RegistrerBrugerButton_Click(object sender, RoutedEventArgs e)
        {
            String firstname = textBoxFirstname.Text;
            String lastname = textBoxLastName.Text;
            String address = textBoxAddress.Text;
            long phonenumber = Convert.ToInt64(textBoxPhonenumber.Text);
            String username = textBoxUsername.Text;
            String password = textBoxPassword.Text;
            String passwordConfirm = textBoxPasswordConfirm.Text;
            if (password.Equals(passwordConfirm))
            {
                try
                {
                    User user = new User(firstname, lastname, address, phonenumber, username, password);
                    if (controller.createUser(user))
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
                    MessageBox.Show(AppResources.UserRegitrationError);
                }
            }
            else
            {
                MessageBox.Show(AppResources.UserPasswordNotEquel);
            }
        }
    }
}