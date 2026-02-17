namespace Pomodoro.Models;



public class PomodoroTimer
{
    public int TimeLeft { get; private set; } = 25 * 60;  // Stores remaining time in seconds
    public bool IsRunning { get; private set; }  // Tells if timer is active

    public bool IsBreak {get; private set; } // Tells if it is break time

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
        
        var remaining = Math.Max(0, TimeLeft - elapsed);

        if (remaining == 0) // If the timer ended
        {
            Skip(); // switch session
            return TimeLeft;
        }

        return remaining;
    }

    public void Reset()  // Reset method
    {
        IsRunning = false;
        _startedAt = null;  // Resets _startedAt

        if (IsBreak) // Check if it's a break
        {
           TimeLeft = 5 * 60;  // Resets TimeLeft  
        }
        else
        {
            TimeLeft = 25 * 60;
        }
    }

    public void Skip()
    {
        bool wasRunning = IsRunning;

        Pause(); // stores remaining time safely

        if (IsBreak)
        {
            TimeLeft = 25 * 60;
            IsBreak = false;
        }
        else
        {
            TimeLeft = 5 * 60;;
            IsBreak = true;
        }

        if (wasRunning)
            Start();
    }
}
