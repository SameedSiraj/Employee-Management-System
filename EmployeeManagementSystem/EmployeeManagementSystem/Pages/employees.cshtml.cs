using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace EmployeeManagementSystem.Pages
{
    public class employeesModel : PageModel
    {
        public List<EmployeeInfo1> listEmployees = new List<EmployeeInfo1>();
        public void OnGet()
        {
            try
            {
                //string connectionString = "\"Data Source=localhost\\SQLEXPRESS;\r\n\r\n   Initial Catalog=employeemanangementsystem";

                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=employeemanagementsystem;Integrated Security=True";


                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM EmployeeInfo";
                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sql, connection))
                    {
                        using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmployeeInfo1 employeeInfo = new EmployeeInfo1();
                                employeeInfo.id = "" + reader.GetInt32(0);
                                employeeInfo.title = reader.GetString(1);
                                employeeInfo.forename = reader.GetString(2);
                                employeeInfo.surname = reader.GetString(3);
                                //employeeInfo.dateOfBirth =  reader.GetDateTime(5).ToString();

                                //DateOnly dateTimeValue = reader.GetDate(5); // Get the DateTime value from the data reader.
                                //DateOnly dateOnlyValue = DateOnly.FromDateTime(dateTimeValue); // Convert it to DateOnly.
                                

                                DateTime abc = reader.GetDateTime(4);
                                employeeInfo.dateOfBirth = DateOnly.FromDateTime(abc).ToString();
                                //employeeInfo.dateOfBirth = reader.GetDateTime(4);
                                //DateOnly dateOnlyValue = DateOnly.FromDateTime(reader.GetDateTime(5));


                                //employeeInfo.dateOfBirth = DateOnly.FromDateTime(reader.GetDateTime(5));

                                employeeInfo.nationalInsuranceNumber = reader.GetString(5);
                                employeeInfo.mobileNumber = reader.GetString(6);
                                employeeInfo.addressDetails = reader.GetString(7);
                                employeeInfo.nextOfKinAddressDetails = reader.GetString(8);
                                //employeeInfo.termsAndConditionsAccepted = reader.GetString(8);
                                employeeInfo.termsAndConditionsAccepted = reader.GetByte(9).ToString();
                                //employeeInfo.photoOrDocument = reader.GetBytes(10) ;
                                employeeInfo.result = reader.GetString(11);

                                listEmployees.Add(employeeInfo);
                        }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class EmployeeInfo
    {
        public string id { get; set; }
        public string title { get; set; }
        public string forename { get; set; }
        public string surname { get; set; }
        public DateOnly dateOfBirth { get; set; }
        public string nationalInsuranceNumber { get; set; }
        public string mobileNumber { get; set; }
        public string addressDetails { get; set; }
        public string nextOfKinAddressDetails { get; set; }
        public int termsAndConditionsAccepted { get; set; }
        public byte[] photoOrDocument { get; set; }
        public string result { get; set; }
    }

    public class EmployeeInfo1 { 
        public string id { get; set; }
        public string title { get; set; }
        public string forename { get; set; }
        public string surname { get; set; }
        public string dateOfBirth { get; set; }
        public string nationalInsuranceNumber { get; set; }
        public string mobileNumber { get; set; }
        public string addressDetails { get; set; }
        public string nextOfKinAddressDetails { get; set; }
        public string termsAndConditionsAccepted { get; set; }
        public string photoOrDocument { get; set; }
        public string result { get; set; }
    }
}
