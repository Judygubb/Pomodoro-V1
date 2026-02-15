
<script setup>  // Written in js
import "../css/TimerView.css";

const props = defineProps([  // Props from presenter
  "timeLeft",   // props.timeLeft --> {{timeLeft}}
  "isRunning"  // props.timeLeft
]);

const emit = defineEmits([  // Events sent to the presenter
  "start",
  "reset",
  "pause"
]);

function formatTime(seconds) {  // Time formatting function
  const m = Math.floor(seconds / 60);  // hehe
  const s = seconds % 60;
  return `${m}:${s.toString().padStart(2, "0")}`;
}
</script>


<template>
  <div class="pomodoro-container"> 
    <div class="tomato">
      <h1 class="timer">{{ formatTime(timeLeft) }}</h1>
    </div>

    <div class="buttons">

      <button class="button button-back" @click="emit('reset')"></button>

      
      <button :class="['button button-play', { running: isRunning }]"
              @click="emit(isRunning ? 'pause' : 'start')"></button>


      <button class="button button-next"></button>
    </div>
  </div>
</template>