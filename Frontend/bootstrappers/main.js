// This the the entry point --> index.jsx

import { createApp } from "vue";
import VueRoot from "./VueRoot.vue";  

console.log("Logging: main");  // Logging

createApp(VueRoot).mount("#root");
