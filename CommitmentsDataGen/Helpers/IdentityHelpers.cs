namespace CommitmentsDataGen.Helpers
{
    public static class IdentityHelpers
    {
        private static long _cohortId = 0;
        private static long _apprenticeshipId = 0;
        private static long _datalockStatusId = 0;

        public static long GetNextCohortId()
        {
            _cohortId++;
            return _cohortId;
        }

        public static long GetNextApprenticeshipId()
        {
            _apprenticeshipId++;
            return _apprenticeshipId;
        }

        public static long GetNextDataLockStatusId()
        {
            _datalockStatusId++;
            return _datalockStatusId;
        }

    }
}
