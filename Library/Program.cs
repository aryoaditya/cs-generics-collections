using System;
using System.Collections.Generic;

public class LibraryApp
{
    private static LibraryCatalog catalog = new LibraryCatalog();
    public static void Main()
    {
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("~~~~~~~~~~~~~~~ SELAMAT DATANG DI PERPUSTAKAAN ONLINE ~~~~~~~~~~~~~~~~~");

        int opsi;
        do
        {
            // Tampilan Menu
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Hapus Buku");
            Console.WriteLine("3. Cari Buku berdasarkan Judul");
            Console.WriteLine("4. Tampilkan Semua Buku");
            Console.WriteLine("5. Keluar");
            Console.Write("Pilih opsi 1-5: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out opsi))
                switch (opsi)
                {
                    case 1:
                        Console.Write("\n~~~~~~~~ Form Tambah Buku ~~~~~~~~\n");
                        // Input tambah buku
                        Console.Write("Judul: ");
                        string title = Console.ReadLine();
                        Console.Write("Penulis: ");
                        string author = Console.ReadLine();
                        Console.Write("Tahun Penerbitan: ");
                        if (int.TryParse(Console.ReadLine(), out int publicationYear))
                        {
                            catalog.AddBook(new Book(title, author,publicationYear));
                            Console.WriteLine("Buku berhasil ditambahkan ke katalog.");
                        }
                        else
                        {
                            // Error handler
                            ErrorHandler.HandleInvalidInput();
                        }
                        break;

                    case 2:
                        Console.Write("Masukkan judul buku yang akan dihapus: ");
                        title = Console.ReadLine();
                        Book bookToRemove = catalog.FindBook(title);

                        if (bookToRemove != null)
                        {
                            catalog.RemoveBook(bookToRemove);
                            Console.WriteLine($"Buku dengan judul '{title}' telah dihapus dari katalog.");
                        }
                        else
                        {
                            // Error handler
                            ErrorHandler.HandleBookNotFound(title);
                        }
                        break;

                    case 3:
                        Console.Write("Masukkan judul buku yang akan dicari: ");
                        title = Console.ReadLine();
                        Book foundBook = catalog.FindBook(title);

                        // Memunculkan list buku jika ada
                        if (foundBook != null)
                        {
                            Console.WriteLine($"Daftar buku yang ditemukan:");
                            Console.WriteLine($"Judul: {foundBook.Title}");
                            Console.WriteLine($"Penulis: {foundBook.Author}");
                            Console.WriteLine($"Tahun Penerbitan: {foundBook.PublicationYear}\n");
                        }
                        else
                        {
                            ErrorHandler.HandleBookNotFound(title);
                        }
                        break;

                    case 4:
                        catalog.ListBooks();
                        break;

                    case 5:
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("~~~~~~ TERIMA KASIH SUDAH MENGUNJUNGI KAMI, SAMPAI JUMPA KEMBALI ~~~~~~");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        break;

                    default:
                        Console.WriteLine("Opsi tidak valid. Silakan pilih kembali angka 1-5.");
                        break;
                }
            else
            {
                Console.WriteLine("Opsi tidak valid. Silakan pilih kembali angka 1-5.");
            }
        } while (opsi != 5);
    }
}

public class LibraryCatalog
{
    // katalog buku dengan collection berbentuk List;
    private List<Book> katalog = new List<Book>();

    // Constructor
    public LibraryCatalog()
    {
        katalog = new List<Book>();
    }

    // Method untuk menambahkan buku ke dalam katalog
    public void AddBook(Book book)
    {
        katalog.Add(book);
    }

    // Method untuk menghapus buku dari katalog
    public void RemoveBook(Book book)
    {
        katalog.Remove(book);
    }

    // Method untuk mencari buku berdasarkan judul
    public Book FindBook(string title)
    {
        for(int i = 0; i < katalog.Count; i++)
        {
            if (katalog[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return katalog[i];
            }
        }

        return null;
    }

    // Method untuk menampilkan daftar semua buku dalam katalog
    public void ListBooks()
    {
        if (katalog.Count > 0)
        {
            Console.WriteLine("Daftar buku:");
            foreach (var book in katalog)
            {
                Console.WriteLine($"Judul: {book.Title}");
                Console.WriteLine($"Penulis: {book.Author}");
                Console.WriteLine($"Tahun Penerbitan: {book.PublicationYear}\n");
            }
        }
        else
            // Untuk menangani jika buku tidak ada dalam katalog
            ErrorHandler.HandleBookListNotFound();
        
    }
}

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }

    // Constructor objek Book
    public Book(string title, string author, int publicationYear)
    {
        Title = title;
        Author = author;
        PublicationYear = publicationYear;
    }
}

// Class 'ErrorHandler' untuk menangani error handling
public class ErrorHandler
{
    // Untuk menangani jika terjadi kesalahan input
    public static void HandleInvalidInput()
    {
        Console.WriteLine("Masukan tidak valid. Silakan coba lagi.\n");
    }

    // Untuk menangani jika buku yang dicari tidak ditemukan
    public static void HandleBookNotFound(string title)
    {
        Console.WriteLine($"Buku dengan judul '{title}' tidak ditemukan dalam katalog.\n");
    }

    // Untuk menangani jika buku yang dicari tidak ditemukan
    public static void HandleBookListNotFound()
    {
        Console.WriteLine($"Tidak ada buku di dalam katalog. Silahkan tambah buku\n");
    }
}
