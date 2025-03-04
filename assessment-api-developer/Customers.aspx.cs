using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using assessment_platform_developer.Services;
using assessment_platform_developer.Validators;
using Container = SimpleInjector.Container;

namespace assessment_platform_developer
{
    public partial class Customers : Page
    {
        private static List<Customer> customers = new List<Customer>();
        private ICustomerService _customerService;
        private ICustomerValidator _customerValidator;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
                _customerService = testContainer.GetInstance<ICustomerService>();
                _customerValidator = testContainer.GetInstance<ICustomerValidator>();

                var allCustomers = _customerService.GetAllCustomers();
                ViewState["Customers"] = allCustomers.ToList();

                PopulateCustomerListBox();
                PopulateCustomerDropDownLists();
            }
            else
            {
                customers = (List<Customer>)ViewState["Customers"];
            }
        }

        private void PopulateCustomerDropDownLists()
        {
            var countryList = Enum.GetValues(typeof(Countries))
                .Cast<Countries>()
                .Select(c => new ListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                })
                .ToArray();

            CountryDropDownList.Items.AddRange(countryList);
            CountryDropDownList.SelectedValue = ((int)Countries.Canada).ToString();
            PopulateStateDropDownList();
        }

        protected void PopulateStateDropDownList()
        {
            StateDropDownList.Items.Clear();
            if (CountryDropDownList.SelectedValue == ((int)Countries.Canada).ToString())
            {
                var provinceList = Enum.GetValues(typeof(CanadianProvinces))
                    .Cast<CanadianProvinces>()
                    .Select(p => new ListItem
                    {
                        Text = p.ToString(),
                        Value = ((int)p).ToString()
                    })
                    .ToArray();
                StateDropDownList.Items.Add(new ListItem(""));
                StateDropDownList.Items.AddRange(provinceList);
            }
            else
            {
                var stateList = Enum.GetValues(typeof(USStates))
                    .Cast<USStates>()
                    .Select(s => new ListItem
                    {
                        Text = s.ToString(),
                        Value = ((int)s).ToString()
                    })
                    .ToArray();
                StateDropDownList.Items.Add(new ListItem(""));
                StateDropDownList.Items.AddRange(stateList);
            }
        }

        protected void PopulateCustomerListBox()
        {
            CustomersDDL.Items.Clear();
            var storedCustomers = customers.Select(c => new ListItem(c.Name)).ToArray();
            if (storedCustomers.Length != 0)
            {
                CustomersDDL.Items.AddRange(storedCustomers);
                CustomersDDL.SelectedIndex = 0;
                return;
            }

            CustomersDDL.Items.Add(new ListItem("Add new customer"));
        }

        /// <summary>
        /// Show provinces based on Country selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CountryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStateDropDownList();
        }

        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            var customer = new Customer
            {
                Name = CustomerName.Text,
                Address = CustomerAddress.Text,
                City = CustomerCity.Text,
                State = StateDropDownList.SelectedValue,
                Zip = CustomerZip.Text,
                Country = CountryDropDownList.SelectedValue,
                Email = CustomerEmail.Text,
                Phone = CustomerPhone.Text,
                Notes = CustomerNotes.Text,
                ContactName = ContactName.Text,
                ContactPhone = CustomerPhone.Text,
                ContactEmail = CustomerEmail.Text
            };

            if (!_customerValidator.Validate(customer))
            {
                MessageLabel.CssClass = "text-danger";
                MessageLabel.Text = "Invalid customer data. Please check the fields and try again.";
                MessageLabel.Visible = true;
                return;
            }
            var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
            var customerService = testContainer.GetInstance<ICustomerService>();
            customerService.AddCustomer(customer);
            customers.Add(customer);

            CustomersDDL.Items.Add(new ListItem(customer.Name));
            ClearFormFields();
            MessageLabel.CssClass = "text-success";
            MessageLabel.Text = "Customer added successfully.";
            MessageLabel.Visible = true;
        }

        /// <summary>
        /// Reset form on submitting the data
        /// </summary>
        private void ClearFormFields()
        {
            CustomerName.Text = string.Empty;
            CustomerAddress.Text = string.Empty;
            CustomerEmail.Text = string.Empty;
            CustomerPhone.Text = string.Empty;
            CustomerCity.Text = string.Empty;
            StateDropDownList.SelectedIndex = 0;
            CustomerZip.Text = string.Empty;
            CountryDropDownList.SelectedIndex = 0;
            CustomerNotes.Text = string.Empty;
            ContactName.Text = string.Empty;
            ContactPhone.Text = string.Empty;
            ContactEmail.Text = string.Empty;
        }
    }
}