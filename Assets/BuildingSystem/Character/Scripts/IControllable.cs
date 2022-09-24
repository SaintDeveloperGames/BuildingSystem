public interface IControllable
{
    public void Run();

    public void Jump(bool isPossibleToJump);

    public void Sit(bool isPressedBttn);

    public void Sprint(bool isPressedBttn);

    public void Gravity(bool isGround);
}
