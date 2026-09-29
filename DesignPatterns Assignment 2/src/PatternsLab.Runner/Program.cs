using System.Diagnostics;
using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;

Console.WriteLine("=== SINGLETON ===\n");

var db = new DatabaseService();
var ui = new UiService();

db.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");
ui.Render();

Console.WriteLine($"Same config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");

Console.WriteLine("\n=== PROTOTYPE ===\n");

Enemy original = new Orc { Name = "Boss Orc" };
var sw = Stopwatch.StartNew();
Enemy copy = original.Clone();
sw.Stop();

Console.WriteLine($"Original model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");
Console.WriteLine($"Clone time:        {sw.ElapsedMilliseconds} ms");

copy.Weapon.Damage = 999;
copy.Abilities.Add("Fire Breath");

Console.WriteLine($"Copy weapon damage: {copy.Weapon.Damage}");
Console.WriteLine($"Original weapon damage: {original.Weapon.Damage}");
Console.WriteLine($"Original abilities: {string.Join(", ", original.Abilities)}");

var registry = new EnemyPrototypeRegistry();
registry.Register("orc", original);
Enemy registryClone = registry.Create("orc");
Console.WriteLine($"Registry clone model id: {registryClone.ModelId}");

Console.WriteLine("\n=== BUILDER ===\n");

var live = RegistrationCallSites.CreateLiveStudent();
var videos = RegistrationCallSites.CreateVideosOnlyStudent();

Console.WriteLine(live);
Console.WriteLine(videos);

Console.WriteLine("\nInvalid combination check:");
try
{
    _ = new CourseRegistration.Builder()
        .ForStudent("test@mail.com")
        .ForCourse("SEF-101")
        .VideosOnly()
        .WithGroupCode("G1")
        .Build();

    Console.WriteLine("ERROR: invalid registration was accepted.");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("\nDone.");
