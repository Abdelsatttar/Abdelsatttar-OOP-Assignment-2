public class Solution
{
    public int MaxVowels(string s, int k)
    {
        int currentCount = 0;
        int maxCount = 0;

        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
            {
                currentCount++;
            }
        }

        maxCount = currentCount;

        for (int right = k; right < s.Length; right++)
        {
            if (IsVowel(s[right]))
            {
                currentCount++;
            }

            if (IsVowel(s[right - k]))
            {
                currentCount--;
            }

            if (currentCount > maxCount)
            {
                maxCount = currentCount;
            }
        }

        return maxCount;
    }

    private bool IsVowel(char character)
    {
        return character == 'a'
            || character == 'e'
            || character == 'i'
            || character == 'o'
            || character == 'u';
    }
}
