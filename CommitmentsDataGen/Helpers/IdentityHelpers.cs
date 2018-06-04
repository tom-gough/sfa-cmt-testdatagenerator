namespace CommitmentsDataGen.Helpers
{
    public static class IdentityHelpers
    {
        private static long _cohortId = 0;
        private static long _apprenticeshipId = 0;

        public static long GetNextCohortId()
        {
            _cohortId++;
            return _cohortId;
        }public static long GetNextApprenticeshipId()
        {
            _apprenticeshipId++;
            return _apprenticeshipId;
        }
    }
}
