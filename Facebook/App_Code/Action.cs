using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

public class Action
{
    string con;
    string cmd;
    SqlDataAdapter dtp;
    DataSet ds;
    public string path;
    public DataRow MainUser;


    public Action()
    {
        con = "Data Source = 'ADRIAN-PC\\SQLExpress'; Database = 'Facebook'; Integrated Security = true";
        cmd = "Select * From [User]";
        dtp = new SqlDataAdapter(cmd, con);
        ds = new DataSet("Facebook");
        try
        {
            dtp.Fill(ds, "User");
        }
        catch { }
    }

    public Action(DataRow MainUser)
    {
        this.MainUser = MainUser;
        con = "Data Source = 'ADRIAN-PC\\SQLExpress'; Database = 'Facebook'; Integrated Security = true";
        cmd = "Select * From [User]";
        dtp = new SqlDataAdapter(cmd, con);
        ds = new DataSet("Facebook");
        try
        {
            dtp.Fill(ds, "User");
        }
        catch { }
    }

    public string Register(string name, string surname, string email, string pass, DateTime birth, string gender)
    {
        if (name.Length == 0) return "Tell me your name";
        else if (surname.Length == 0) return "Last name?";
        else if (email.Length == 0) return "We need an E-mail address of yours";
        else if (pass.Length < 8 || pass[0].ToString() != pass[0].ToString().ToUpper() || !(pass.Contains('0') || pass.Contains('1') || pass.Contains('2') || pass.Contains('3') || pass.Contains('4') || pass.Contains('5') || pass.Contains('6') || pass.Contains('7') || pass.Contains('8') || pass.Contains('9')))
            return "Password needs to be\n1)At least 8 symbols length\n2)First letter must be Upper case\n3)Must contain at least one number";
        else if (gender.Length == 0) return "Maybe you can also tell us your gender??";
        if (CheckEmail(email)) return "User with this E-mail is already registered!";
        if (Directory.Exists(path + "/Users/" + email)) Directory.Delete(path + "/Users/" + email, true);
        Directory.CreateDirectory(path + "/Users/" + email + "/Messages");
        Directory.CreateDirectory(path + "/Users/" + email + "/Photos");
        Image pic = Image.FromFile(path + "/" + gender + ".jpg");
        //File.Copy(path + "/" + gender + ".jpg", path + "/Users/" + email + "/Profile.jpg", true);
        cmd = string.Format("INSERT INTO [User] Values('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},'{8}','','','','','')", name, surname, email, gender, pass, birth.Day, birth.Month, birth.Year, pic);
        dtp = new SqlDataAdapter(cmd, con);
        dtp.Fill(ds, "User");
        Load();
        return "You are registered\nYou can now login using your E-mail and Password";
    }

    public bool Login(string email, string pass)
    {
        DataRow row = GetUserData(email);
        if (row == null) return false;
        if (row["Password"].ToString() == pass)
        {
            return true;
        }
        return false;
    }

    public void Load()
    {
        ds = new DataSet("Facebook");
        cmd = "SELECT * FROM [User]";
        dtp = new SqlDataAdapter(cmd, con);
        dtp.Fill(ds, "User");
    }

    public bool CheckEmail(string Email)
    {
        dtp.Fill(ds, "User");
        foreach (DataRow row in ds.Tables["User"].Rows)
        {
            if (row["Email"].ToString() == Email) return true;
        }
        return false;
    }

    public string GetUserDirectory(string Email)
    {
        return path + "/Users/" + Email;
    }

    public string GetUserDirectory()
    {
        return GetUserDirectory(MainUser["Email"].ToString());
    }

    public string RecoverPassword(string Email)
    {
        dtp.Fill(ds, "User");
        foreach (DataRow row in ds.Tables["User"].Rows)
        {
            if (row["Email"].ToString() == Email) return row["Password"].ToString();
        }
        return null;
    }

    public void Edit(string name, string surname, string email, string pass, DateTime birth, string home, string job, string school, string college, string university)
    {
        cmd = string.Format("UPDATE [User] SET Name='{0}', Surname='{1}', Email='{11}', Password='{2}', Day={3}, Month={4}, Year={5}, HomeTown='{6}', Job='{7}', School='{8}', College='{9}', University='{10}' WHERE Email='{11}'", name, surname, pass, birth.Day, birth.Month, birth.Year, home, job, school, college, university, email);
        dtp = new SqlDataAdapter(cmd, con);
        dtp.Fill(ds, "User");
        Load();
        //if (Path.GetExtension(Picture.PostedFile.FileName).ToLower() != ".jpg" && Path.GetExtension(Picture.PostedFile.FileName).ToLower() != ".png")
        //    return;
        //if (File.Exists(path + "/Users/" + MainUser["Email"].ToString() + "/Profile.jpg"))
        //    File.Delete(path + "/Users/" + MainUser["Email"].ToString() + "/Profile.jpg");
        //Picture.PostedFile.SaveAs(path + "/Users/" + MainUser["Email"].ToString() + "/Profile.jpg");
        //DateTime date = DateTime.Now;
        //File.WriteAllText(path + "/Users/" + MainUser["Email"] + "/Profile.data", date.ToShortDateString() + "/" + date.Hour + "." + date.Minute + "." + date.Second);
        ////File.WriteAllText(path + "/Users/" + MainUser["Email"] + "/Profile.dat", Picture.Value);
        //ResetLikes();
    }

    public DataRow GetUserData(string email)
    {
        cmd = "Select * From [User]";
        dtp = new SqlDataAdapter(cmd, con);
        ds = new DataSet("Facebook");
        dtp.Fill(ds, "User");
        try
        {
            foreach (DataRow row in ds.Tables["User"].Rows)
            {
                if (row["Email"].ToString() == email)
                {
                    return row;
                }
            }
        }
        catch { }
        return null;
    }

    public void DeleteUserDirectory(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        foreach (var f in dir.GetFiles())
            f.Delete();
        foreach (var d in dir.GetDirectories())
            DeleteUserDirectory(d.FullName);
        dir.Delete();
    }

    public void DeleteUser(string Email)
    {
        cmd = string.Format("Delete From [User] WHERE Email='{0}'", Email);
        dtp = new SqlDataAdapter(cmd, con);
        ds = new DataSet("Facebook");
        dtp.Fill(ds, "User");
        DeleteUserDirectory(GetUserDirectory(Email));
    }

    public void SendFriendRequest(string email)
    {
        List<String> arr = GetFriendRequests(email);
        arr.Add(MainUser["Email"].ToString());
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(path + "/Users/" + email + "/FriendRequests.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                bf.Serialize(fs, arr);
            }
        }
        catch { }
    }

    public void DeleteFriendRequest(string email, bool fromMainUser)
    {
        List<String> arr = new List<string>();
        BinaryFormatter bf = new BinaryFormatter();
        if (fromMainUser)
        {
            arr = GetFriendRequests();
            arr.Remove(email);
            try
            {
                using (FileStream fs = new FileStream(GetUserDirectory() + "/FriendRequests.dat", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    bf.Serialize(fs, arr);
                }
            }
            catch { }
        }
        else
        {
            arr = GetFriendRequests(email);
            arr.Remove(MainUser["Email"].ToString());
            try
            {
                using (FileStream fs = new FileStream(path + "/Users/" + email + "/FriendRequests.dat", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    bf.Serialize(fs, arr);
                }
            }
            catch { }
        }
    }

    public void AddFriend(string email)
    {
        List<String> arr = GetFriends();
        arr.Add(email);
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(GetUserDirectory() + "/Friends.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                bf.Serialize(fs, arr);
            }
        }
        catch
        { }
        arr = GetFriends(email);
        arr.Add(MainUser["Email"].ToString());
        try
        {
            using (FileStream fs = new FileStream(path + "/Users/" + email + "/Friends.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                bf.Serialize(fs, arr);
            }
        }
        catch
        { }
        DeleteFriendRequest(email, true); //from mainuser
        DeleteFriendRequest(email, false); //from secondary user
    }

    public void DeleteFriend(string email)
    {
        List<String> arr = GetFriends();
        arr.Remove(email);
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(GetUserDirectory() + "/Friends.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                bf.Serialize(fs, arr);
            }
        }
        catch
        { }
        arr = GetFriends(email);
        if (arr == null) return;
        arr.Remove(MainUser["Email"].ToString());
        try
        {
            using (FileStream fs = new FileStream(path + "/Users/" + email + "/Friends.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                bf.Serialize(fs, arr);
            }
        }
        catch
        { }
    }

    public List<String> GetFriendRequests()
    {
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(GetUserDirectory() + "/FriendRequests.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (List<String>)bf.Deserialize(fs);
            }
        }
        catch { return new List<String>(); }
    }

    public List<String> GetFriendRequests(string email)
    {
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(path + "/Users/" + email + "/FriendRequests.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (List<String>)bf.Deserialize(fs);
            }
        }
        catch { return new List<String>(); }
    }

    public List<String> GetFriends()
    {
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(GetUserDirectory() + "/Friends.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (List<String>)bf.Deserialize(fs);
            }
        }
        catch { return new List<String>(); }
    }

    public List<String> GetFriends(string email)
    {
        if (!CheckEmail(email)) return null;
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(path + "/Users/" + email + "/Friends.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (List<String>)bf.Deserialize(fs);
            }
        }
        catch { return new List<String>(); }
    }

    public void LikeUnlike(string email)
    {
        if (!CheckEmail(email)) return;
        List<String> arr = GetLikes(email);
        if (arr != null)
        {
            if (arr.Contains(MainUser["Email"].ToString())) arr.Remove(MainUser["Email"].ToString());
            else arr.Add(MainUser["Email"].ToString());
        }
        else
        {
            arr = new List<string>();
            arr.Add(MainUser["Email"].ToString());
        }
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(path + "/Users/" + email + "/Likes.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                bf.Serialize(fs, arr);
            }
        }
        catch { }
    }

    public List<String> GetLikes()
    {
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(GetUserDirectory() + "/Likes.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (List<String>)bf.Deserialize(fs);
            }
        }
        catch { return new List<String>(); }
    }

    public List<String> GetLikes(string email)
    {
        if (!CheckEmail(email)) return new List<String>();
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(path + "/Users/" + email + "/Likes.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (List<String>)bf.Deserialize(fs);
            }
        }
        catch { return new List<String>(); }
    }

    public void ResetLikes()
    {
        File.WriteAllText(GetUserDirectory() + "/Likes.dat", "");
    }

    public string ReadMessages(DataRow us)
    {
        try
        {
            return File.ReadAllText(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/" + us["Email"].ToString() + ".dat");
        }
        catch
        {
            return "";
        }
    }

    public string ReadMessages(DataRow main, DataRow us)
    {
        try
        {
            return File.ReadAllText(path + "/Users/" + main["Email"].ToString() + "/Messages/" + us["Email"].ToString() + "/" + us["Email"].ToString() + ".dat");
        }
        catch
        {
            return "";
        }
    }

    public void WriteMessage(DataRow us, string txt)
    {
        string text = ReadMessages(us);
        text += MainUser["Name"].ToString() + ": " + txt + "<br/>";
        try
        {
            File.WriteAllText(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/" + us["Email"].ToString() + ".dat", text);
        }
        catch
        {
            Directory.CreateDirectory(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/");
            File.WriteAllText(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/" + us["Email"].ToString() + ".dat", text);
        }
        text = ReadMessages(us, MainUser);
        text += MainUser["Name"].ToString() + ": " + txt + "</br>";
        try
        {
            File.WriteAllText(path + "/Users/" + us["Email"].ToString() + "/Messages/" + MainUser["Email"].ToString() + "/" + MainUser["Email"].ToString() + ".dat", text);
        }
        catch
        {
            Directory.CreateDirectory(path + "/Users/" + us["Email"].ToString() + "/Messages/" + MainUser["Email"].ToString() + "/");
            File.WriteAllText(path + "/Users/" + us["Email"].ToString() + "/Messages/" + MainUser["Email"].ToString() + "/" + MainUser["Email"].ToString() + ".dat", text);
        }
        ////New Message Notification!!!////
        File.WriteAllText(path + "/Users/" + us["Email"].ToString() + "/Messages/" + MainUser["Email"].ToString() + "/new_message.dat", "");
        ///////////////////////////////////
    }

    public List<DataRow> CheckNewMessages()
    {
        List<DataRow> arr = new List<DataRow>();
        try
        {
            foreach (DirectoryInfo d in new DirectoryInfo(GetUserDirectory() + "/Messages/").GetDirectories())
            {
                if (File.Exists(d.FullName + "/" + "/new_message.dat"))
                {
                    if (GetUserData(path + "/Users/" + d.Name + "/" + d.Name + ".dat") != null) arr.Add(GetUserData(path + "/Users/" + d.Name + "/" + d.Name + ".dat"));
                }
            }
        }
        catch { }
        return arr;
    }

    public bool CheckNewMessage(DataRow us)
    {
        if (File.Exists(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/new_message.dat"))
        {
            try
            {
                File.Delete(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/new_message.dat");
            }
            catch
            {
                File.WriteAllText(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/new_message.dat", "");
                File.Delete(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/new_message.dat");
            }
            return true;
        }
        return false;
    }

    public void DeleteAllMessages(DataRow us)
    {
        try
        {
            File.WriteAllText(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/" + us["Email"].ToString() + ".dat", "");
        }
        catch
        {
            Directory.CreateDirectory(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/");
            File.WriteAllText(GetUserDirectory() + "/Messages/" + us["Email"].ToString() + "/" + us["Email"].ToString() + ".dat", "");
        }
    }

    public void UploadPhoto(HttpPostedFile Photo, string Title, string Location)
    {
        if (Path.GetExtension(Photo.FileName).ToLower() != ".jpg" && Path.GetExtension(Photo.FileName).ToLower() != ".png")
            return;
        DateTime date = DateTime.Now;
        string filePath = GetUserDirectory() + "/Photos/" + date.ToLongDateString() + "/" + date.Hour + "." + date.Minute + "." + date.Second;
        Directory.CreateDirectory(filePath);
        Photo.SaveAs(filePath + "/Photo.jpg");
        Photo ph = new Photo(Title, Location, date, date.ToLongDateString() + "/" + date.Hour + "." + date.Minute + "." + date.Second);
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = new FileStream(filePath + "/Photo.dat", FileMode.OpenOrCreate, FileAccess.Write))
        {
            bf.Serialize(fs, ph);
        }
        File.WriteAllText(filePath + "/Likes.dat", "");
    }

    public void UploadPhoto(string Path, string Title, string Location)
    {
        if (!Path.ToLower().EndsWith(".jpg") && !Path.ToLower().EndsWith(".png"))
            return;
        DateTime date = DateTime.Now;
        string filePath = GetUserDirectory() + "/Photos/" + date.ToShortDateString() + "/" + date.Hour + "." + date.Minute + "." + date.Second;
        Directory.CreateDirectory(filePath);
        File.Copy(Path, filePath + "/Photo.jpg", true);
        Photo ph = new Photo(Title, Location, date, date.ToShortDateString() + "/" + date.Hour + "." + date.Minute + "." + date.Second);
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = new FileStream(filePath + "/Photo.dat", FileMode.OpenOrCreate, FileAccess.Write))
        {
            bf.Serialize(fs, ph);
        }
        File.WriteAllText(filePath + "/Likes.dat", "");
    }

    public Photo GetPhotoData(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
        {
            return (Photo)bf.Deserialize(fs);
        }
    }

    public void DeletePhoto(string Path)
    {
        try
        {
            DirectoryInfo current = new DirectoryInfo(GetUserDirectory() + "/Photos/" + Path);
            DirectoryInfo parent = Directory.GetParent(current.FullName);
            Directory.Delete(current.FullName, true);
            Directory.Delete(Directory.GetParent(GetUserDirectory() + "/Photos/" + Path).FullName);
        }
        catch { }
    }

    public List<String> GetPhotoLikes(string userToGetLikes, string photoPath)
    {
        List<String> arr = new List<string>();
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            using (FileStream fs = new FileStream(path + "/Users/" + userToGetLikes + "/Photos/" + photoPath + "/Likes.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                arr = (List<String>)bf.Deserialize(fs);
            }
            return arr;
        }
        catch { return arr; }
    }

    public void LikeUnlikePhoto(string userToLike, string photoPath)
    {
        if (!CheckEmail(userToLike)) return;
        List<String> arr = GetPhotoLikes(userToLike, photoPath);
        if (arr != null)
        {
            if (arr.Contains(MainUser["Email"].ToString())) arr.Remove(MainUser["Email"].ToString());
            else arr.Add(MainUser["Email"].ToString());
        }
        else
        {
            arr = new List<string>();
            arr.Add(MainUser["Email"].ToString());
        }
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = new FileStream(path + "/Users/" + userToLike + "/Photos/" + photoPath + "/Likes.dat", FileMode.OpenOrCreate, FileAccess.Write))
        {
            bf.Serialize(fs, arr);
        }
    }

    public List<Photo> GetUserPhotos(string Email)
    {
        if (GetUserData(Email) == null) return null;
        List<Photo> arr = new List<Photo>();
        foreach (DirectoryInfo d in new DirectoryInfo(path + "/Users/" + Email + "/Photos").GetDirectories())
        {
            foreach (DirectoryInfo dd in d.GetDirectories())
            {
                foreach (FileInfo f in dd.GetFiles())
                {
                    if (f.Name == "Photo.dat")
                    {
                        arr.Add(GetPhotoData(f.FullName));
                    }
                }
            }
        }
        return SortByDateTime.SortDescending(arr);
    }

    public void WriteUserOnline()
    {
        File.WriteAllText(GetUserDirectory() + "/Online.dat", DateTime.Now.ToString());
    }

    public String UserLastOnline(string Email)
    {
        if (File.Exists(path + "/Users/" + Email + "/Online.dat"))
        {
            try
            {
                return File.ReadAllText(path + "/Users/" + Email + "/Online.dat");
            }
            catch { }
        }
        return null;
    }

    public bool IsUserOnline(string Email)
    {
        if (File.Exists(path + "/Users/" + Email + "/Online.dat"))
        {
            if (DateTime.Now.ToShortTimeString() == DateTime.Parse(UserLastOnline(Email)).ToShortTimeString() && DateTime.Parse(UserLastOnline(Email)).ToShortDateString() == DateTime.Now.ToShortDateString())
                return true;
        }
        return false;
    }

    public void MakeProfilePicture(string Picture)
    {
        File.WriteAllText(GetUserDirectory() + "/Profile.dat", Picture + "/Photo.jpg");
        ResetLikes();
    }

    public string ReadProfilePicture(string Email)
    {
        return File.ReadAllText(GetUserDirectory(Email) + "/Profile.dat");
    }

    public string GetProfilePicture(string Email)
    {
        if (!File.Exists(GetUserDirectory(Email) + "/Profile.dat") || !File.Exists(GetUserDirectory(Email) + "/Photos/" + File.ReadAllText(GetUserDirectory(Email) + "/Profile.dat")))
            return "~/Data/" + GetUserData(Email)["Gender"] + ".jpg";
        return "~/Data/Users/" + Email + "/Photos/" + ReadProfilePicture(Email);
    }

    public bool IsProfilePicture(string Path)
    {
        try
        {
            if (ReadProfilePicture(MainUser["Email"].ToString()) == Path)
                return true;
        }
        catch
        { }
        return false;
    }
}