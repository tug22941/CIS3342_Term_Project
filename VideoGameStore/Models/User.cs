using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoGameStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String UserType { get; set; }
        public User(int id, String firstName, String lastName, String username, String password, String email, String phone, String address, String city, String state, String zip, String userType)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Email = email;
            Phone = phone;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            UserType = userType;
        }
        public User(int id)
        {
            Id = id;
            FirstName = "";
            LastName = "";
            Username = "";
            Password = "";
            Email = "";
            Phone = "";
            Address = "";
            City = "";
            State = "";
            Zip = "";
            UserType = "";
        }
    }
    
}
