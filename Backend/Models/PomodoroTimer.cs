namespace Pomodoro.Models;

public class PomodoroTimer
{
    public int TimeLeft { get; private set; } = 25 * 60;  // Stores remaining time in seconds
    public bool IsRunning { get; private set; }  // Tells if timer is active

    private DateTime? _startedAt;  // Stores when timer started

    public void Start()  // Runs when user presses start
    {
        if (IsRunning) return;

        IsRunning = true;  // Starts timer
        _startedAt = DateTime.UtcNow;  // Stores current time plz work
    }

    public void Pause()
    {
        if (!IsRunning || _startedAt == null) return;

        TimeLeft -= (int)(DateTime.UtcNow - _startedAt.Value).TotalSeconds;

        IsRunning = false;
        _startedAt = null;
    }

    public int GetTimeLeft()  // Used by controller when frontend asks timer state
    {
        if (!IsRunning || _startedAt == null)  // If the timer is paused, return TimeLeft
            return TimeLeft;

        var elapsed = (int)(DateTime.UtcNow - _startedAt.Value).TotalSeconds;
        return Math.Max(0, TimeLeft - elapsed);  // Prevents negativ time
    }

    public void Reset()  // Reset method
    {
        IsRunning = false;
        TimeLeft = 25 * 60;  // Resets TimeLeft
        _startedAt = null;  // Resets _startedAt
    }
}
