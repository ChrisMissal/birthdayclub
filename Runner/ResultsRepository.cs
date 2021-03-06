namespace Runner
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Core;
    using Headspring;

    class ResultsRepository
    {
        private readonly string _fileName;
        private readonly IEnumerable<Birthday> _birthdays;

        public ResultsRepository(string fileName)
        {
            _fileName = fileName;
            _birthdays = File.ReadAllLines(_fileName)
                .Select(x => x.Split(new [] { ',', '/' }))
                .Select(x => new Birthday(int.Parse(x[0].Trim()), int.Parse(x[1].Trim())));
        }

        public Birthday NextBirthday()
        {
            if (!_birthdays.Any())
                return new Birthday(SystemClock.UtcNow.Month, SystemClock.UtcNow.Day);

            var lastBirthday = _birthdays.Last();
            return lastBirthday;
        }

        public void Save(Gift gift)
        {
            var birthday = gift.Receiver.Birthday;
            File.AppendAllLines(_fileName, new[] {
                string.Format("{0}/{1},{2},{3}", birthday.Month, birthday.Day, gift.Gifter.Name, gift.Receiver.Name)
            });
        }
    }
}