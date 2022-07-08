namespace RBUtils.Extension
{
    public static class DateTimeEx
    {
        public static int GetAge(this DateTime dateOfBirth, DateTime? now)
        {
            if (now == null)
            {
                now = DateTime.Now;
            }

            return (int)Math.Floor(((DateTime)now).Subtract(dateOfBirth).Days / 365.24219);
        }
    }
}
