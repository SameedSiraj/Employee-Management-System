using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace EmployeeManagementSystem.Pages
{
    public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            try
            {
                employeeInfo.title = Request.Form["title"];
                employeeInfo.forename = Request.Form["forename"];
                employeeInfo.surname = Request.Form["surname"];
                //employeeInfo.dateOfBirth = Request.Form["dateOfBirth"];
                string dateOfBirthString = Request.Form["dateOfBirth"];
                if (System.DateOnly.TryParse(dateOfBirthString, out System.DateOnly dateOfBirth))
                {
                    // Parsing was successful, you can now assign the dateOfBirth to your employeeInfo object.
                    employeeInfo.dateOfBirth = dateOfBirth;
                }

                employeeInfo.nationalInsuranceNumber = Request.Form["dateOfBirth"];
                employeeInfo.mobileNumber = Request.Form["mobileNumber"];
                employeeInfo.addressDetails = Request.Form["addressDetails"];
                employeeInfo.nextOfKinAddressDetails = Request.Form["nextOfKinAddressDetails"];
                employeeInfo.termsAndConditionsAccepted = Request.Form["termsAndConditions"] == "yes" ? 1 : 0;
                //employeeInfo.photoOrDocument = Request.Form["photoOrDocument"];



                //HttpPostedFile file = Request.Files["photoOrDocument"]; // Assuming you have an input field with the name "photoOrDocument"

                //if (file != null && file.ContentLength > 0)
                //{
                //    byte[] binaryData;

                //    using (BinaryReader binaryReader = new BinaryReader(file.InputStream))
                //    {
                //        binaryData = binaryReader.ReadBytes(file.ContentLength);
                //    }

                //    // Now you can assign the binary data to your property
                //    employeeInfo.photoOrDocument = binaryData;
                //}
                







                //string connectionString = Configuration.GetConnectionString("EmployeeConnection");
                                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=employeemanagementsystem;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SQL query to insert the employee data into the database
                    string insertQuery = "INSERT INTO EmployeeInfo (Title, Forename, Surname, DateOfBirth, NationalInsuranceNumber, MobileNumber, AddressDetails, NextOfKinAddressDetails, TermsAndConditionsAccepted, Result) " +
                                         "VALUES (@Title, @Forename, @Surname, @DateOfBirth, @NationalInsuranceNumber, @MobileNumber, @AddressDetails, @NextOfKinAddressDetails, @TermsAndConditionsAccepted, @Result)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        System.DateOnly dateOfBirth1 = employeeInfo.dateOfBirth;
                        DateTime dateOfBirthAsDateTime = new DateTime(dateOfBirth1.Year, dateOfBirth1.Month, dateOfBirth1.Day);

                        DateOnly dateOfBirth2 = employeeInfo.dateOfBirth; // Replace with the person's actual date of birth
                        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now); // Get the current date

                        int age = currentDate.Year - dateOfBirth2.Year;

                        if (age < 18)
                        {
                            successMessage = "Your application is being considered";
                            employeeInfo.result = "Your application is being considered";
                        }
                        else if (age > 18 && age < 30)
                        {
                            successMessage = "Your application has been accepted";
                            employeeInfo.result = "Your application is being considered";
                        }
                        else
                        {
                            successMessage = "Your application is unsuccessful";
                            employeeInfo.result = "Your application is being considered";
                        }


                        // Add parameters to the query
                        command.Parameters.AddWithValue("@Title", employeeInfo.title);
                        command.Parameters.AddWithValue("@Forename", employeeInfo.forename);
                        command.Parameters.AddWithValue("@Surname", employeeInfo.surname);
                        command.Parameters.AddWithValue("@DateOfBirth", dateOfBirthAsDateTime);
                        command.Parameters.AddWithValue("@NationalInsuranceNumber", employeeInfo.nationalInsuranceNumber);
                        command.Parameters.AddWithValue("@MobileNumber", employeeInfo.mobileNumber);
                        command.Parameters.AddWithValue("@AddressDetails", employeeInfo.addressDetails);
                        command.Parameters.AddWithValue("@NextOfKinAddressDetails", employeeInfo.nextOfKinAddressDetails);
                        command.Parameters.AddWithValue("@TermsAndConditionsAccepted", employeeInfo.termsAndConditionsAccepted);
                        //command.Parameters.AddWithValue("@PhotoOrDocument", employeeInfo.photoOrDocument);
                        command.Parameters.AddWithValue("@Result", employeeInfo.result);

                        // Execute the SQL command to insert the employee data
                        command.ExecuteNonQuery();

                       
                    }
                }

            }
            catch(Exception ex) { }
            }
    }
}
