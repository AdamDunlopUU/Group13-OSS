using System;

namespace OnlineShopSystem
{
    public class User
    {
        // Properties
        public int UserID { get; private set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string UserStreet { get; set; }
        public string UserCity { get; set; }

        // Constructor
        public User(int userID, string userName, string userPassword, string userEmail, string phoneNumber, string userStreet, string userCity)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName), "Username cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(userPassword)) throw new ArgumentNullException(nameof(userPassword), "Password cannot be null or empty.");

            UserID = userID;
            UserName = userName;
            UserPassword = userPassword;
            UserEmail = userEmail ?? string.Empty;
            PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? string.Empty : phoneNumber;
            UserStreet = userStreet ?? string.Empty;
            UserCity = userCity ?? string.Empty;
        }

        public void UpdateProfile(string newUserName, string newUserEmail, string newPhoneNumber, string newStreet, string newCity)
        {
            UserName = !string.IsNullOrWhiteSpace(newUserName) ? newUserName : UserName;
            UserEmail = !string.IsNullOrWhiteSpace(newUserEmail) ? newUserEmail : UserEmail;
            PhoneNumber = !string.IsNullOrWhiteSpace(newPhoneNumber) ? newPhoneNumber : PhoneNumber;
            UserStreet = !string.IsNullOrWhiteSpace(newStreet) ? newStreet : UserStreet;
            UserCity = !string.IsNullOrWhiteSpace(newCity) ? newCity : UserCity;

            Console.WriteLine("Profile updated successfully.");
        }

        public void Logout()
        {
            Console.WriteLine($"{UserName} has logged out.");
        }

        public override string ToString()
        {
            return $"UserID: {UserID}, Name: {UserName}, Email: {UserEmail}, Phone: {PhoneNumber}, Address: {UserStreet}, {UserCity}";
        }
    }
}
