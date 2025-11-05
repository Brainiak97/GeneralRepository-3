namespace UserService.Domain
{
    public class ActiveUsersCounter
    {
        private long _doctorsLoggedIn = 0;
        private long _patientsLoggedIn = 0;

        public void IncrementDoctor() => Interlocked.Increment(ref _doctorsLoggedIn);
        public void IncrementPatient() => Interlocked.Increment(ref _patientsLoggedIn);

        public long GetDoctorsCount() => Interlocked.Read(ref _doctorsLoggedIn);
        public long GetPatientsCount() => Interlocked.Read(ref _patientsLoggedIn);
    }
}
