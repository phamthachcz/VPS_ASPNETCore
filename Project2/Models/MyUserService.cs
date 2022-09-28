using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Models
{
    public class MyUserService
    {
        public async Task<int> getMaxId()
        {
            List<MyUser> user = await this.List();
            int max = 0;
            if(user.Count == 0)
            {
                max = 1;
            }
            else
            {
                max = user[user.Count - 1].Id + 1;
            }
            return max;
        }

        public async Task<int> getIdUser(string email)
        {
            List<MyUser> users = await this.List();
            foreach(MyUser user in users)
            {
                if(user.Email.ToLower() == email)
                {
                    return user.Id;
                }
            }
            
            return 0;
        }

        public async Task<MyUser> getUserByEmail(string email)
        {
            List<MyUser> users = await this.List();
            
            return users.First(x => x.Email == email); 
        }

        public async Task<MyUser> getUserById(int id)
        {
            List<MyUser> users = await this.List();

            return users.First(x => x.Id == id);
        }

        public async Task CreateUser(MyUser user)
        {
            List<MyUser> users;
            if (System.IO.File.Exists("users.json"))
            {
                string json = await System.IO.File.ReadAllTextAsync("users.json");

                users = JsonConvert.DeserializeObject<List<MyUser>>(json);
                if (users == null)
                {
                    users = new List<MyUser>();
                }
            }
            else
            {
                users = new List<MyUser>();
            }
            users.Add(user);
            await System.IO.File.AppendAllTextAsync("users.json", JsonConvert.SerializeObject(users));
        }

        public async Task EditUser(MyUser user)
        {
            List<MyUser> users;
            if (System.IO.File.Exists("users.json"))
            {
                string json = await System.IO.File.ReadAllTextAsync("users.json");

                users = JsonConvert.DeserializeObject<List<MyUser>>(json);
                if (users == null)
                {
                    users = new List<MyUser>();
                }
                else
                {
                    foreach(MyUser item in users)
                    {
                        if(item.Id == user.Id)
                        {
                            item.Email = user.Email;
                            item.Password = user.Password;
                        }
                    }
                }
            }
            else
            {
                users = new List<MyUser>();
            }
            await System.IO.File.WriteAllTextAsync("users.json", JsonConvert.SerializeObject(users));
        }

        public async Task<List<MyUser>> List()
        {
            List<MyUser> users;
            if (System.IO.File.Exists("users.json"))
            {
                string json = await System.IO.File.ReadAllTextAsync("users.json");

                users = JsonConvert.DeserializeObject<List<MyUser>>(json);
                if (users == null)
                {
                    users = new List<MyUser>();
                }
                return users;
            }
            else
            {
                users = new List<MyUser>();
                return users;
            }
            
        }
    }
}
