
<script setup>  // Written in js
import "../css/TimerView.css";
import { watch } from "vue";

const clickSound = new Audio("/sounds/click.mp3");
const timerDoneSound = new Audio("/sounds/timer_done.mp3");



function playClick() {
  clickSound.currentTime = 0;
  clickSound.play();
}

const props = defineProps([  // Props from presenter
  "timeLeft",   // props.timeLeft --> {{timeLeft}}
  "isRunning",  // props.timeLeft
  "isBreak"
]);


const emit = defineEmits([  // Events sent to the presenter
  "start",
  "reset",
  "pause",
  "skip"
]);

function formatTime(seconds) {  // Time formatting function
  const m = Math.floor(seconds / 60); 
  const s = seconds % 60;
  return `${m}:${s.toString().padStart(2, "0")}`;
}

watch(() => props.timeLeft, (newTime, oldTime) => {
  if (oldTime <= 1 && newTime > oldTime) {
    timerDoneSound.currentTime = 0;
    timerDoneSound.play().catch(() => {});
  }
});

</script>


<template>
  <div :class="['background', { break: isBreak }]">
    <div class="pomodoro-container"> 
      <div :class="['tomato', { break: isBreak }]">
        <h1 class="timer">{{ formatTime(timeLeft) }}</h1>
      </div>

      <div class="buttons">

        <button class="button button-back" @click="() => { playClick(); emit('reset'); }"></button>

        
        <button :class="['button button-play', { running: isRunning }]"
                  @click="() => { playClick(); emit(isRunning ? 'pause' : 'start'); }">
        </button>


        <button class="button button-skip" @click="() => { playClick(); emit('skip'); }"></button>
      </div>
    </div>
  </div>
</template>