namespace EnterpriseDT.Util.Debug;

public interface CustomLogLevelAppender : Appender
{
    Level CurrentLevel { get; set; }
}
