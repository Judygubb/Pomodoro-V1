import { h, reactive, onMounted } from "vue";
import TimerView from "../Views/TimerView.vue";


const state = reactive({ // This stores the UI state
  timeLeft: 2500,  // Placeholder values until the backend loads
  isRunning: false,
  isBreak: false
});

async function fetchTimer() { // Loads timer data
  try {
    const res = await fetch("http://localhost:5269/api/pomodoro");  // Request backend --> GET/api/pomodoro
    const data = await res.json();  // Converts response, JSON to JS objects
    state.timeLeft = data.timeLeft;  // Updates states
    state.isRunning = data.isRunning;
    state.isBreak = data.isBreak;
  } catch (error) {  // Error handling
    console.error('Failed to fetch timer:', error);
  }
}

async function start() {  // Starts timer
  await fetch("http://localhost:5269/api/pomodoro/start", { method: "POST" });  // GET/api/pomodoro/start
  fetchTimer();  // Refreshes UI
}

async function pause() {  // Pauses timer
  await fetch("http://localhost:5269/api/pomodoro/pause", { method: "POST" });  // GET/api/pomodoro/start
  fetchTimer(); 
}

async function reset() {  // Resets timer
  await fetch("http://localhost:5269/api/pomodoro/reset", { method: "POST" });  // GET/api/pomodoro/reset
  fetchTimer();
}

async function skip() {  // Switches timer
  await fetch("http://localhost:5269/api/pomodoro/skip", { method: "POST" });  // GET/api/pomodoro/skip
  fetchTimer();  
}

const PomodoroMain = {  // Presenter component
  name: "Timer",
  setup() {  // Runs when a comopnent is loaded
    onMounted(() => {  // Runs when component appears on screen
      fetchTimer();  // Loads timer state immediately 
      setInterval(fetchTimer, 1000);  // Pool backend every second
    });
    return { state, start, pause, reset, skip };  // setup returns these states
  },
  render() {
    return h(TimerView, {  // Rendering the timer view
      timeLeft: state.timeLeft,  // Props to the view
      isRunning: state.isRunning,
      isBreak: state.isBreak,
      onStart: start,  // Passing event handlers
      onReset: reset,
      onPause: pause,
      onSkip: skip,
    });
  }
};

export { PomodoroMain };