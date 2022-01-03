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
using BookLotModel;
using System.Data.Entity;
using System.Data;

namespace Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     enum ActionState
{
             New,
             Edit,
            Delete,
            Nothing
}
public partial class MainWindow : Window
    {
    
        //using BookLotModel;
        ActionState action = ActionState.Nothing;
        BookLotEntitiesModel ctx = new BookLotEntitiesModel();
        CollectionViewSource clientVSource;
        CollectionViewSource depositVSource;
        CollectionViewSource clientOrdersVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            clientVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            clientVSource.Source = ctx.Clients.Local;
            ctx.Clients.Load();
           
            depositVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("depositViewSource")));
            depositVSource.Source = ctx.Deposits.Local;
            ctx.Deposits.Load();

            clientOrdersVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("clientOrdersViewSource")));
            clientOrdersVSource.Source = ctx.Orders.Local;
            //ctx.Orders.Load();
            cmbClients.ItemsSource = ctx.Clients.Local;
            //cmbClients.DisplayMemberPath = "FirstName";
            cmbClients.SelectedValuePath = "ClientId";
            //cmbDeposit.ItemsSource = ctx.Deposits.Local;
            //cmbDeposit.DisplayMemberPath = "Author";
            //cmbDeposit.SelectedValuePath = "BookId";
            BindDataGrid();

        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            BindingOperations.ClearBinding(firstNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(addressTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(emailTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(phoneNumberTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(authorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(titleTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(publishingHouseTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(priceTextBox, TextBox.TextProperty);

            SetValidationBinding();
        }
        private void btnEditO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(firstNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(addressTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(emailTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(phoneNumberTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(authorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(titleTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(publishingHouseTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(priceTextBox, TextBox.TextProperty);

            SetValidationBinding();
        }
        private void btnDeleteO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToNext();
            depositVSource.View.MoveCurrentToNext();
            clientOrdersVSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToPrevious();
            depositVSource.View.MoveCurrentToPrevious();
            clientOrdersVSource.View.MoveCurrentToPrevious();
        }
        private void SaveClients()
        {
            Client client = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Client entity
                    client = new Client()
                    {
                        FirstName = firstNameTextBox.Text.Trim(),
                        LastName = lastNameTextBox.Text.Trim(),
                        Address = addressTextBox.Text.Trim(),
                        Email = emailTextBox.Text.Trim(),
                        PhoneNumber = phoneNumberTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Clients.Add(client);
                    clientVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                    
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    client = (Client)clientDataGrid.SelectedItem;
                    client.FirstName = firstNameTextBox.Text.Trim();
                    client.LastName = lastNameTextBox.Text.Trim();
                    client.Address = addressTextBox.Text.Trim();
                    client.Email = emailTextBox.Text.Trim();
                    client.PhoneNumber = phoneNumberTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    client = (Client)clientDataGrid.SelectedItem;
                    ctx.Clients.Remove(client);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientVSource.View.Refresh();
            }
        }
        private void SaveDeposits()
        {
            Deposit deposit = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Deposit entity
                    deposit = new Deposit()
                    {
                        Author = authorTextBox.Text.Trim(),
                        Title = titleTextBox.Text.Trim(),
                        PublishingHouse = publishingHouseTextBox.Text.Trim(),
                        Price = decimal.Parse(priceTextBox.Text.Trim())
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Deposits.Add(deposit);
                    depositVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    deposit = (Deposit)depositDataGrid.SelectedItem;
                    deposit.Author = authorTextBox.Text.Trim();
                    deposit.Title = titleTextBox.Text.Trim();
                    deposit.PublishingHouse = publishingHouseTextBox.Text.Trim();
                    deposit.Price = decimal.Parse(priceTextBox.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    deposit = (Deposit)depositDataGrid.SelectedItem;
                    ctx.Deposits.Remove(deposit);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                depositVSource.View.Refresh();
            }
        }
        private void SaveOrders()
        {
            Order order = null;
            if (action == ActionState.New)
            {
                try
                {
                    Client client = (Client)cmbClients.SelectedItem;
                    //Deposit deposit = (Deposit)cmbDeposit.SelectedItem;
                    //instantiem Order entity
                    order = new Order()
                    {
                        ClientId = client.ClientId,
                        //BookId = deposit.BookId
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Orders.Add(order);
                    //salvam modificarile
                    ctx.SaveChanges();
                    BindDataGrid();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
      if (action == ActionState.Edit)
            {
                dynamic selectedOrder = ordersDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedOrder.OrderId;
                    var editedOrder = ctx.Orders.FirstOrDefault(s => s.OrderId == curr_id);
                    if (editedOrder != null)
                    {
                        editedOrder.ClientId = Int32.Parse(cmbClients.SelectedValue.ToString());
                        //editedOrder.BookId = Convert.ToInt32(cmbDeposit.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                // pozitionarea pe item-ul curent
                clientVSource.View.MoveCurrentTo(selectedOrder);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedOrder = ordersDataGrid.SelectedItem;
                    int curr_id = selectedOrder.OrderId;
                    var deletedOrder = ctx.Orders.FirstOrDefault(s => s.OrderId == curr_id);
                    if (deletedOrder != null)
                    {
                        ctx.Orders.Remove(deletedOrder);
                        ctx.SaveChanges();
                        MessageBox.Show("Order Deleted Successfully", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void BindDataGrid()
        {
            var queryOrder = from ord in ctx.Orders
                             join client in ctx.Clients on ord.ClientId equals
                             client.ClientId
                             join dep in ctx.Deposits on ord.BookId
                             equals dep.BookId
                             /*join ordcont in ctx.OrdersContent on ord.OrderContentId
                             equals ordcont.OrderContentId*/
                             join cup in ctx.Coupon on ord.CouponId
                             equals cup.CouponId
                             select new
                             {
                                 ord.OrderId,
                                 ord.BookId,
                                 ord.ClientId,
                                 //ord.OrderContentId,
                                 client.FirstName,
                                 client.LastName,
                                 client.Address,
                                 client.Email,
                                 client.PhoneNumber,
                                 ord.Total,
                                 ord.Quantity,
                                 dep.Author,
                                 dep.Title,
                                 dep.PublishingHouse,
                                 dep.Price,
                                 cup.Code,
                                 cup.Value,
                                 cup.NumberOfUses,
                                 cup.Text
                             };
            clientOrdersVSource.Source = queryOrder.ToList();
        }

        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
                
        private void ReInitialize()
        {
            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tblCtrlBookLot.SelectedItem as TabItem;
            switch (ti.Header)
            {
                case "Clients":
                    SaveClients();
                    break;
                case "Deposit":
                    SaveDeposits();
                    break;
                case "Orders":
                    break;
            }
            ReInitialize();
        }
        private void SetValidationBinding()
        {
            Binding firstNameValidationBinding = new Binding();
            firstNameValidationBinding.Source = clientVSource;
            firstNameValidationBinding.Path = new PropertyPath("FirstName");
            firstNameValidationBinding.NotifyOnValidationError = true;
            firstNameValidationBinding.Mode = BindingMode.TwoWay;
            firstNameValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string required
            firstNameValidationBinding.ValidationRules.Add(new StringNotEmpty());
            firstNameTextBox.SetBinding(TextBox.TextProperty, firstNameValidationBinding);
            Binding lastNameValidationBinding = new Binding();
            lastNameValidationBinding.Source = clientVSource;
            lastNameValidationBinding.Path = new PropertyPath("LastName");
            lastNameValidationBinding.NotifyOnValidationError = true;
            lastNameValidationBinding.Mode = BindingMode.TwoWay;
            lastNameValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string min length validator
            lastNameValidationBinding.ValidationRules.Add(new StringMinLengthValid());
            lastNameTextBox.SetBinding(TextBox.TextProperty, lastNameValidationBinding); //setare binding nou
        }
    }
}
