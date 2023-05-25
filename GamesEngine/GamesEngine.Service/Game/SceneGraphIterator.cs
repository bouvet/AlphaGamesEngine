namespace GamesEngine.Service.Game;

public interface IIterator
{
    IWalkerStyle Walker { get; }
}

public interface IWalkerStyle
{
    IWalkerHandler Handler { get; }
}

public interface IWalkerHandler
{

}

public interface WalkerStyleA : IWalkerStyle
{

}

public interface WalkerStyleB : IWalkerStyle
{

}