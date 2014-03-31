using System.Collections.Generic;
using service.Domain;

namespace service
{
    public static class Storage
    {
        //not thread safe I know!!! this is for demo purposes!!!
        internal static List<FullUser> Users = new List<FullUser>(new[]
        {
            new FullUser
            {
                Id = 1,
                Name = "Name1",
                Username = "Username1",
                Email = "email1@gmail.com",
                Role = "user",
                Password = "password1"
            },
            new FullUser
            {
                Id = 2,
                Name = "Name2",
                Username = "Username2",
                Email = "email2@gmail.com",
                Role = "admin",
                Password = "password2"
            },
            new FullUser
            {
                Id = 3,
                Name = "Name3",
                Username = "Username3",
                Email = "email3@gmail.com",
                Role = "user",
                Password = "password3"
            }
        });
         
    }
}