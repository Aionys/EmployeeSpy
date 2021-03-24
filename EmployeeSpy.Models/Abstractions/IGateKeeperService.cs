namespace EmployeeSpy.Core.Abstractions
{
    public interface IGateKeeperService
    {
        bool VerifyVisitorPassAttempt(int personId, int gateKeeperId);

        bool VerifyEmployeePassAttempt(int personId, int gateKeeperId);
    }
}
