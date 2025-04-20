using System.Text;

namespace AgeDll;

public static class Age
{
    public static (int years, int months, int days) CalculateAge(DateTime birthDate, DateTime targetDate)
    {
        // Ensure reference date is after birth date
        if (targetDate < birthDate)
            throw new ArgumentException("current date must be after birth date");

        int years = targetDate.Year - birthDate.Year;
        int months, days;

        // Check if we need to subtract a year
        if (targetDate.Month < birthDate.Month || (targetDate.Month == birthDate.Month && targetDate.Day < birthDate.Day))
            years--;

        // Calculate months
        months = targetDate.Month - birthDate.Month;
        if (months < 0)
            months += 12;

        // Calculate days
        days = targetDate.Day - birthDate.Day;
        if (days < 0)
        {
            DateTime previousMonth = targetDate.AddMonths(-1);
            days += DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);
            months--; // Adjust months if we borrowed a month for days
        }

        return (years, months, days);
    }

    public static string GetAgeDescription(int years, int months, int days)
    {
        if (years > 0 || months > 0 || days > 0)
        {
            StringBuilder result = new("Age: ");
            bool isFirstPart = true;

            if (years > 0)
            {
                result.Append($"{years} years");
                isFirstPart = false;
            }

            if (months > 0)
            {
                if (!isFirstPart)
                    result.Append(" . ");

                result.Append($"{months} months");
                isFirstPart = false;
            }

            if (days > 0)
            {
                if (!isFirstPart)
                    result.Append(" . ");

                result.Append($"{days} days");
            }

            return result.ToString();
        }
        else
            return "Age information not available or zero.";
    } 
}
