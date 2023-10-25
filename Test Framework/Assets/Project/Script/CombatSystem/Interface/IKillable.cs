/// <summary>
/// GameManager와 같은 매니저 클래스에서 대상을 즉시 죽이기 위한 인터페이스
/// </summary>
public interface IKillable
{
    bool IsDead { get; }
    void Kill();
}