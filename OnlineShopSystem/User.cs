// Rachel Wallace

using System;
using OnlineShopSystem;

namespace OnlineShopSystem;

public class User
{
    internal string Email;
    internal int ID;

    // properties
    public int UserID { get; private set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public string UserEmail { get; set; }
    public string PhoneNumber { get; set; }
    public string UserStreet { get; set; }
    public string UserCity { get; set; }
    public object Id { get; internal set; }

    public User(int userID, string userName, string userPassword, string userEmail, string phoneNumber, string userStreet, string userCity)
    {
        UserID = userID;
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        UserPassword = userPassword ?? throw new ArgumentNullException(nameof(userPassword));
        UserEmail = userEmail ?? throw new ArgumentNullException(nameof(userEmail));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
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

    // display user details
    public override string ToString()
    {
        return $"UserID: {UserID}, Name: {UserName}, Email: {UserEmail}, Phone: {PhoneNumber}, Address: {UserStreet}, {UserCity}";

    }
}
