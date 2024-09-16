using System;
using System.Threading.Tasks;

namespace Menu_Pesanan
{
    class Program
    {
        // Daftar menu makanan dan minuman
        static string[] menuMakanan = { 
            "Nasi Goreng", 
            "Nasi Uduk", 
            "Nasi Kucing", 
            "Mie Rebus", 
            "Mie Goreng", 
            "Mie Ayam" };
        static int[] hargaMakanan = {
            12000, 
            9000, 
            3000, 
            9000, 
            9000, 
            13000 };

        static string[] menuMinuman = { 
            "Teh Botol", 
            "Teh Pucuk", 
            "Susu Jahe", 
            "Kopi Jahe", 
            "Kopi Susu", 
            "Tea Jus" };
        static int[] hargaMinuman = { 
            5000, 
            4000, 
            6000, 
            3000, 
            5000, 
            0 };

        static async Task Main(string[] args)
        {
            bool running = true;
            double totalHarga = 0;

            while (running)
            {
                Console.Clear();
                TampilkanMenu();
                Console.Write("\nMasukkan kode menu (1a, 2b) atau ketik 'q' untuk keluar: ");
                string input = Console.ReadLine().ToLower();

                if (input == "q")
                {
                    running = false;
                    break;
                }

                // Split input berdasarkan koma (untuk memilih beberapa menu sekaligus)
                string[] kodeMenuList = input.Split(',');

                foreach (string kodeMenu in kodeMenuList)
                {
                    string trimmedKodeMenu = kodeMenu.Trim(); // Hapus spasi ekstra
                    if (trimmedKodeMenu.Length == 2)
                    {
                        char kategori = trimmedKodeMenu[0];
                        char pilihan = trimmedKodeMenu[1];

                        if (kategori == '1' && pilihan >= 'a' && pilihan <= 'f')
                        {
                            int indexMenu = pilihan - 'a';  // Menghitung index untuk menu makanan
                            totalHarga += HitungHargaMakanan(indexMenu);
                        }
                        else if (kategori == '2' && pilihan >= 'a' && pilihan <= 'f')
                        {
                            int indexMenu = pilihan - 'a';  // Menghitung index untuk menu minuman
                            totalHarga += HitungHargaMinuman(indexMenu);
                        }
                        else
                        {
                            Console.WriteLine($"Kode menu {trimmedKodeMenu} tidak valid.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Kode menu {trimmedKodeMenu} tidak valid.");
                    }
                }

                Console.WriteLine($"Total sementara: Rp.{totalHarga}");

                // Validasi apakah ingin melanjutkan atau tidak
                Console.Write("\nApakah Anda ingin menambahkan pesasan? [y/n] ");
                string lanjut = Console.ReadLine().ToLower();

                if (lanjut == "n")
                {
                    running = false;
                }


            }

            double diskon = 0;
            double TaxRate = 0;

            if (totalHarga >= 100000) {
                    Console.WriteLine("\nSelamat anda mendapatkan diskon 20% karena sudah memesan lebih dari Rp.100000");
                    diskon =  totalHarga * 0.20;
                    totalHarga = totalHarga - diskon;
                    TaxRate = totalHarga * 0.10;
                    totalHarga = totalHarga + TaxRate;
                    Console.WriteLine($"\nTotal yang harus dibayar (sudah termasuk ppn 10% dan potongan diskon): Rp.{totalHarga}");
                    Pembayaran();
            } 
            else if (totalHarga < 100000)
            {
                TaxRate = totalHarga * 0.10;
                totalHarga = totalHarga + TaxRate;
                Console.WriteLine($"\nTotal yang harus dibayar (sudah termasuk ppn 10%): Rp.{totalHarga}");
                await Pembayaran();
            } 
        }

        static async Task Pembayaran()
        {
            Console.WriteLine("\n1. E-Wallet");
            Console.WriteLine("2. Kartu");
            Console.Write("Pilih metode pembayaran: ");
            int paymentMethod = int.Parse(Console.ReadLine());

            switch (paymentMethod)
            {
                case 1:
                    Console.WriteLine("Anda memilih pembayaran E-Wallet.");
                    break;
                case 2:
                    Console.WriteLine("Anda memilih pembayaran dengan kartu.");
                    break;
                default:
                    Console.WriteLine("Metode pembayaran tidak valid.");
                    return;
            }
            await ProsesPembayaranAsync();

            async Task ProsesPembayaranAsync()
            {
                Console.WriteLine("Memproses pembayaran...");

                // Simulasi proses yang memakan waktu lama dengan Task.Delay
                await Task.Delay(3000); // Simulasi menunggu 3 detik (misalnya pembayaran sedang diproses)

                Console.WriteLine("Pembayaran berhasil.");
                Console.WriteLine("\n+" + new string('=', 31) + "+");
                Console.WriteLine("|          TERIMA KASIH         |");
                Console.WriteLine("+" + new string('=', 31) + "+");
            }
        }

        // Fungsi untuk menampilkan menu
        static void TampilkanMenu()
        {
            Console.WriteLine("+" + new string('=', 75) + "+");
            Console.WriteLine("|                                 MENU PESANAN                                |");
            Console.WriteLine("+" + new string('=', 75) + "+");
            Console.WriteLine("|            MENU MAKANAN              |            MENU MAKANAN              |");
            Console.WriteLine("+" + new string('=', 75) + "+");

            int maxLength = Math.Max(menuMakanan.Length, menuMinuman.Length);

            for (int i = 0; i < maxLength; i++)
            {
                string makanan = i < menuMakanan.Length ? $"1{(char)('a' + i)}. {menuMakanan[i],-20} - Rp.{hargaMakanan[i],5}" : "";
                string minuman = i < menuMinuman.Length ? $"2{(char)('a' + i)}. {menuMinuman[i],-20} - Rp.{hargaMinuman[i],5}" : "";

                Console.WriteLine($"| {makanan,-36} | {minuman,-36} |");
            }

            Console.WriteLine("+" + new string('-', 75) + "+");
        }

        // Fungsi untuk menghitung harga makanan
        static int HitungHargaMakanan(int index)
        {
            if (index >= 0 && index < hargaMakanan.Length)
            {
                Console.Write($"Berapa banyak {menuMakanan[index]} yang dipesan: ");
                int jumlah = int.Parse(Console.ReadLine());
                return hargaMakanan[index] * jumlah;
            }
            return 0;
        }

        // Fungsi untuk menghitung harga minuman
        static int HitungHargaMinuman(int index)
        {
            if (index >= 0 && index < hargaMinuman.Length)
            {
                Console.Write($"Berapa banyak {menuMinuman[index]} yang dipesan: ");
                int jumlah = int.Parse(Console.ReadLine());
                return hargaMinuman[index] * jumlah;
            }
            return 0;
        }
    }
}
