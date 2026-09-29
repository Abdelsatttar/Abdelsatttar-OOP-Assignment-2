namespace SrpLab;

public sealed class GradeCsvExporter
{
    public string Export(GradeBook book, LetterGradePolicy letterPolicy, HonorRollPolicy honorPolicy)
    {
        var rows = new List<string> { "studentId,average,letter,honor" };
        foreach (var id in book.Scores.Keys.OrderBy(x => x))
        {
            var average = book.Average(id);
            var letter = letterPolicy.GetLetter(average);
            var honor = honorPolicy.Meets(average, letter);
            rows.Add($"{id},{average},{letter},{(honor ? 1 : 0)}");
        }
        return string.Join('\n', rows);
    }
}
