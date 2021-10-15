using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace BasicAuthentication
{
    public class Program : User // Inheritan dari User <Sebagai Child>
    {
        public static void MenuUtama(List<User> ListUser)
        {
            int exit = 0;
            do
            {
                Console.WriteLine("\t====================================");
                Console.WriteLine("\t\tBASIC AUTHENTICATION");
                Console.WriteLine("\t===================================");
                Console.Write("\n");
                Console.Write("\n");
                Console.WriteLine("========= DAFTAR MENU =========");
                Console.WriteLine("1. Show Akun");
                Console.WriteLine("2. Delete Akun");
                Console.WriteLine("3. Upadate Akun");
                Console.WriteLine("4. Search Akun");
                Console.WriteLine("5. Exit ??");
                Console.WriteLine("================================");
                Console.WriteLine("Pilihan ANDA ? >");
                Console.WriteLine("Masukkan Pilihan Anda! ");
                try
                {
                    int pilih = int.Parse(Console.ReadLine());
                    switch (pilih)
                    {
                        case 1:
                            Console.Clear();
                            ShowUser(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            Delete(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case 3:
                            Console.Clear();
                            Update(ListUser);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 4:
                            Console.Clear();
                            Search(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case 5:
                            Console.Clear();
                            Keluar(ListUser);
                            Console.Clear();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("\t====================================");
                            Console.WriteLine("\t\tBASIC AUTHENTICATION");
                            Console.WriteLine("\t===================================");
                            Console.Write("\n");
                            Console.Write("Angka yang Anda Salah ! Mohon Ulangi Input Pilihan");
                            Console.Write("\n");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("\t====================================");
                    Console.WriteLine("\t\tBASIC AUTHENTICATION");
                    Console.WriteLine("\t===================================");
                    Console.Write("\n");
                    Console.Write("Inputan Anda Bukan Angka!!, Ulangi Input Pilihan");
                    Console.Write("\n");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            while (exit != 10);

        }
        public static void CreateUser(List<User> ListUser)
        {
            Console.WriteLine("\t====================================");
            Console.WriteLine("\t\tBASIC AUTHENTICATION");
            Console.WriteLine("\t===================================");
            Console.Write("\n");
            Console.WriteLine("======Create User======\n");

            bool cekData = false;

            string namaDepan, namaBelakang;
            do
            {
            Console.Write("Nama Depan = ");
            namaDepan = Console.ReadLine();

            Console.Write("Nama Belakang = ");
            namaBelakang = Console.ReadLine();
                if (namaDepan == "" || namaBelakang == "")
                {
                    Console.WriteLine("Data Pastikan Semua Data Terisi!!!");
                    Console.ReadLine();
                    Console.Clear();
                    CreateUser(ListUser);
                }
                else
                {
                    cekData = true;
                }

            } while (cekData== false);

            Random rnd = new Random();
            int rPertama = rnd.Next(1, 99);   // creates a number between 1 and 99
            int rKedua = rnd.Next(100);     // creates a number between 0 and 100
            string uName = namaDepan.Substring(0,2) + namaBelakang.Substring(0,2) +rPertama + rKedua;
            Console.Write($"Username =  {uName} \n");
        z:
            Console.Write("Input Password <Min 8 karater > = ");
            string pass = Console.ReadLine();

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var isValidated = hasNumber.IsMatch(pass) && hasUpperChar.IsMatch(pass) && hasMinimum8Chars.IsMatch(pass);
            if (isValidated)
            {
                pass= BCrypt.Net.BCrypt.HashPassword(pass);
            }
            else
            {
                Console.WriteLine("Password Harus Gabungan Angka, Huruf dan Simbol");
                goto z;
            }

            ListUser.Add(new User(namaDepan, namaBelakang, uName, pass));
            Console.WriteLine($"\n\t\t\tSelamat {namaDepan} {namaBelakang} Data Anda Berhasil Ditambahkan!!");
            Console.WriteLine("\t\t=========================================================================");
            Console.WriteLine($"\t\t\t\tSilahkan Login  Username : {uName}");
            Console.ReadKey();
            Console.Clear();
            LoginUser(ListUser);

        }
        public static void ShowUser(List<User> ListUser)
        {
            Console.WriteLine("\t====================================");
            Console.WriteLine("\t\tBASIC AUTHENTICATION");
            Console.WriteLine("\t===================================");
            Console.Write("\n");
            Console.WriteLine("\n======Show User======\n");

            foreach (var item in ListUser)
            {
                Console.WriteLine("=================================");
                Console.WriteLine($"Nama Depan : {item.firstName}");
                Console.WriteLine($"Nama Belakang : {item.lastName}");
                Console.WriteLine($"Username : {item.userName}");
                Console.WriteLine($"Password : {item.password}");
                Console.WriteLine("=================================");
            }
        }
        public static void Delete(List<User> ListUser)
        {
            Console.WriteLine("\t====================================");
            Console.WriteLine("\t\tBASIC AUTHENTICATION");
            Console.WriteLine("\t===================================");
            Console.Write("\n");
            Console.WriteLine("\n======Delete Akun======\n");
            Console.Write(" Input UserName : ");
            string usernm = Console.ReadLine();

            bool exist = ListUser.Exists(item => item.userName == usernm);
            if (exist)
            {
                Console.WriteLine("Apakah Anda Yakin Ingin Menghapus data Ini ?? [Y] / [N] ");
                string Pil = Console.ReadLine();
                if (Pil == "Y")
                {
                    ListUser.RemoveAll(item => item.userName == usernm);
                    Console.WriteLine("\n Data Berhasil Di hapus");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Data Gagal Di hapus");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Data Tidak Ditemukan!!!");
            }
        }
        public static void Update(List<User> ListUser)
        {
            Console.WriteLine("\t====================================");
            Console.WriteLine("\t\tBASIC AUTHENTICATION");
            Console.WriteLine("\t===================================");
            Console.Write("\n");
            Console.WriteLine("\n======Update Akun======\n");
            Console.Write("Input Username : ");
            string a = Console.ReadLine();

            User c = ListUser.FirstOrDefault(x => x.userName == a);
                if (c == null)
            {
                Console.WriteLine("User Tidak Ditemukan");
                return;
            }
            else
            {
                foreach (var item in ListUser)
                {
                    if (item.userName == a)
                    {
                        Console.WriteLine("\n=== Tampil Data User === \n");
                        Console.WriteLine($"Nama Depan : {item.firstName}" );
                        Console.WriteLine($"Nama Belakang :  {item.lastName}");
                        Console.WriteLine($"UserName :   {item.userName}");
                        Console.WriteLine($"Password : {item.password}\n\n");

                        // Define a regular expression for repeated words.
                        var hasNumber = new Regex(@"[0-9]+");
                        var hasUpperChar = new Regex(@"[A-Z]+");
                        var hasMinimum8Chars = new Regex(@".{8,}");
                        z:
                        Console.WriteLine("Password Baru :");
                        string newPass = Console.ReadLine();
                        var isValidated = hasNumber.IsMatch(newPass) && hasUpperChar.IsMatch(newPass) && hasMinimum8Chars.IsMatch(newPass);
                        if (isValidated)
                        {
                            item.password = BCrypt.Net.BCrypt.HashPassword(newPass);
                            Console.WriteLine("\nPassword Has been Changed!");
                            Console.WriteLine();
                        }
                        else {
                            Console.WriteLine("Password Harus Gabungan Angka, Huruf dan Simbol");
                            goto z;
                        }
                    }
                }
            }
        }
        public static void Search(List<User> ListUser)
        {
            Console.WriteLine("\t====================================");
            Console.WriteLine("\t\tBASIC AUTHENTICATION");
            Console.WriteLine("\t===================================");
            Console.Write("\n");
            Console.WriteLine("\n======Show Akun======\n");
            Console.Write("Input Username : ");
            string a = Console.ReadLine();

            User c = ListUser.FirstOrDefault(x => x.userName == a);
            if (c == null)
            {
                Console.WriteLine("User Tidak Ditemukan");
                return;
            }
            else
            {
                foreach (var item in ListUser)
                {
                    if (item.userName == a)
                    {
                        Console.WriteLine("\n===== Tampil Data User ===== \n");
                        Console.WriteLine($"Nama Depan : {item.firstName}");
                        Console.WriteLine($"Nama Belakang :  {item.lastName}");
                        Console.WriteLine($"UserName :   {item.userName}");
                        Console.WriteLine($"Password : {item.password}\n\n");
                    }
                }
            }
        }
        public static void LoginUser(List<User> ListUser)
        {
            Console.WriteLine("\t====================================");
            Console.WriteLine("\t\tBASIC AUTHENTICATION");
            Console.WriteLine("\t===================================");
            Console.Write("\n");
            Console.WriteLine("\n======Login User======\n");
            Console.Write("Input Username : ");
            string user = Console.ReadLine();

            Console.Write("Input Password : ");
            string pass = Console.ReadLine();
            if (ListUser.Exists(item => item.userName == user && BCrypt.Net.BCrypt.Verify(pass ,item.password)))
            {
                Console.WriteLine("\n\t====Login Berhasil!!====");
                Console.ReadKey();
                Console.Clear();
                MenuUtama(ListUser);
            }
            else
            {
                Console.WriteLine("Username Tidak Ditemukan");
            }
        }
        public static void Keluar(List<User> ListUser)
        {
            Console.WriteLine("\t====================================");
            Console.WriteLine("\t\tBASIC AUTHENTICATION");
            Console.WriteLine("\t===================================");
            Console.Write("\n");
            Console.WriteLine("ANDA KELUAR!!");
            Console.WriteLine("");
            Console.Write("Press any key to continue . . . \n");
            Console.ReadKey(true);
        }

        public static void Main(string[] args)
        {
            int exit = 0;

            //nampung data List data
            List<User> ListUser = new List<User>();
            ListUser.Add(new User() {firstName = "Hamid", lastName= "saja", userName = "hasa", password ="123" });
            do
            {
                Console.WriteLine("\t====================================");
                Console.WriteLine("\t\tBASIC AUTHENTICATION");
                Console.WriteLine("\t===================================");
                Console.Write("\n");
                Console.Write("\n");
                Console.WriteLine("========= MENU PILIHAN =========");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Show Akun");
                Console.WriteLine("3. Serach Akun");
                Console.WriteLine("4. Login");
                Console.WriteLine("5. Exit ??");
                Console.WriteLine("================================");
                Console.WriteLine("Pilihan ANDA ? >");
                Console.WriteLine("Masukkan Pilihan Anda! ");
                try
                {
                    int pilih = int.Parse(Console.ReadLine());
                    switch (pilih)
                    {
                        case 1:
                            Console.Clear();
                            CreateUser(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            ShowUser(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 3:
                            Console.Clear();
                            Search(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                            
                        case 4:
                            Console.Clear();
                            LoginUser(ListUser);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 5:
                            Console.Clear();
                            Keluar(ListUser);
                            Console.Clear();    
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("\t====================================");
                            Console.WriteLine("\t\tBASIC AUTHENTICATION");
                            Console.WriteLine("\t===================================");
                            Console.Write("\n");
                            Console.Write("Angka Salah ! Ulangi Input Pilihan");
                            Console.Write("\n");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }
                catch (FormatException )
                {
                    Console.Clear();
                    Console.WriteLine("\t====================================");
                    Console.WriteLine("\t\tBASIC AUTHENTICATION");
                    Console.WriteLine("\t===================================");
                    Console.Write("\n");
                    Console.Write("Inputan Anda Salah!! Ulangi Input Pilihan");
                    Console.Write("\n");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            while (exit != 10);
        }
    }
    }


    

