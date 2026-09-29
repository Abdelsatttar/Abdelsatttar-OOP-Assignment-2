using LibrarySystem.Models;

Console.WriteLine("=== Library System - Inheritance Demo ===");

var student = new StudentMember("ST-001", "Ali Hassan", "01000000001");
var premium = new PremiumMember("PR-001", "Sara Ahmed", "01000000002", 20);

var book = new Book("B-001", "Clean Code", 5m);
var dvd = new DVD("D-001", "C# Fundamentals", 10m);
var magazine = new Magazine("M-001", "Tech Monthly", 6m);
var withdrawnBook = new Book("B-002", "Old Book", 5m);
withdrawnBook.Withdraw();

Console.WriteLine("\n--- Must NOT compile examples ---");
Console.WriteLine("The following lines are intentionally commented out because they must NOT compile.");
// var plainPerson = new Person("P-1", "Name", "0100"); // must NOT compile
// var plainMember = new Member("M-1", "Name", "0100", 3, 0); // must NOT compile
// var plainStaff = new Staff("S-1", "Name", "0100", DateTime.Today, 10000, 0); // must NOT compile
// var plainItem = new LibraryItem("I-1", "Item", 7, 5, 1); // must NOT compile
// student.FullName = "Changed"; // must NOT compile
// student.Loans.Add(null!); // must NOT compile
// book.IsOnLoan = true; // must NOT compile

var validationStaff = new Librarian("TEMP-001", "Temporary", "01000000006", DateTime.Today, 5000m);

Console.WriteLine("\n--- Positive value validation ---");
try
{
    validationStaff.GiveRaise(0);
}
catch (Exception ex)
{
    Console.WriteLine($"Zero raise: {ex.Message}");
}

try
{
    dvd.SetBaseLateFee(-1);
}
catch (Exception ex)
{
    Console.WriteLine($"Negative late fee: {ex.Message}");
}

Console.WriteLine("\n--- Borrow validation ---");
try
{
    student.Borrow(withdrawnBook, new DateTime(2026, 9, 1));
}
catch (Exception ex)
{
    Console.WriteLine($"Borrow withdrawn item: {ex.Message}");
}

student.Borrow(book, new DateTime(2026, 9, 1));
try
{
    premium.Borrow(book, new DateTime(2026, 9, 1));
}
catch (Exception ex)
{
    Console.WriteLine($"Borrow an item already on loan: {ex.Message}");
}

var secondStudentItem = new Book("B-003", "Algorithms", 5m);
var thirdStudentItem = new Book("B-004", "Databases", 5m);
var fourthStudentItem = new Book("B-005", "Networks", 5m);
student.Borrow(secondStudentItem, new DateTime(2026, 9, 1));
student.Borrow(thirdStudentItem, new DateTime(2026, 9, 1));
try
{
    student.Borrow(fourthStudentItem, new DateTime(2026, 9, 1));
}
catch (Exception ex)
{
    Console.WriteLine($"Student fourth loan: {ex.Message}");
}

Console.WriteLine("\n--- Staff monthly pay polymorphism ---");
var librarian = new Librarian("L-001", "Nora Samir", "01000000003", new DateTime(2025, 1, 10), 9000m);
var head = new HeadLibrarian("H-001", "Omar Adel", "01000000004", new DateTime(2023, 4, 15), 12000m);
var shelver = new Shelver("S-001", "Mona Ali", "01000000005", new DateTime(2026, 2, 1), 7000m, "Fiction");

var staffMembers = new List<Staff> { librarian, head, shelver };
foreach (var staff in staffMembers)
    Console.WriteLine($"{staff.FullName}: monthly pay = {staff.GetMonthlyPay():0.00}");

head.SetLateFee(magazine, 8m);
Console.WriteLine($"Magazine new daily late fee: {magazine.GetDailyLateFee():0.00}");

shelver.Reassign("Children");
Console.WriteLine($"Shelver section: {shelver.Section}");

Console.WriteLine("\n--- Item polymorphism ---");
var items = new List<LibraryItem> { book, dvd, magazine };
foreach (var item in items)
    Console.WriteLine($"{item.CatalogNumber}: period={item.LoanPeriodDays} days, daily fee={item.GetDailyLateFee():0.00}");

Console.WriteLine("\n--- Raise action ---");
var oldSalary = shelver.MonthlySalary;
shelver.GiveRaise(10);
Console.WriteLine($"Shelver salary: {oldSalary:0.00} -> {shelver.MonthlySalary:0.00}");

Console.WriteLine("\n--- Premium late return ---");
var premiumDvdLoan = premium.Borrow(dvd, new DateTime(2026, 9, 1));
Console.WriteLine($"Due date: {premiumDvdLoan.DueDate:yyyy-MM-dd}");
librarian.ProcessReturn(premiumDvdLoan, new DateTime(2026, 9, 13));
Console.WriteLine($"Late fee: {premiumDvdLoan.LateFee:0.00}");
Console.WriteLine($"Reading points: {premium.ReadingPoints}");

Console.WriteLine("\n--- Illegal status changes ---");
try
{
    librarian.ProcessReturn(premiumDvdLoan, new DateTime(2026, 9, 13));
}
catch (Exception ex)
{
    Console.WriteLine($"Return same loan twice: {ex.Message}");
}

try
{
    librarian.MarkAsLost(premiumDvdLoan);
}
catch (Exception ex)
{
    Console.WriteLine($"Mark returned loan as lost: {ex.Message}");
}

Console.WriteLine("\n--- Invalid return date ---");
var invalidDateItem = new Book("B-007", "Dates", 5m);
var invalidDateLoan = premium.Borrow(invalidDateItem, new DateTime(2026, 9, 10));
try
{
    librarian.ProcessReturn(invalidDateLoan, new DateTime(2026, 9, 9));
}
catch (Exception ex)
{
    Console.WriteLine($"Return before borrow date: {ex.Message}");
}

Console.WriteLine("\n--- Lost loan ---");
var lostItem = new Book("B-006", "Lost Book", 5m);
var lostLoan = premium.Borrow(lostItem, new DateTime(2026, 9, 1));
librarian.MarkAsLost(lostLoan);
Console.WriteLine($"{lostLoan.LoanId} status = {lostLoan.Status}, item on loan = {lostLoan.Item.IsOnLoan}");

Console.WriteLine("\nDone.");
