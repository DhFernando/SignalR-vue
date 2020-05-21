<template>
  <div id="HelloWorld">
    <div class="topBar">
      <span class="topBarTitle">My landry shop</span>
      <b-button @click="toggle =! toggle" class="taskButton" size="is-small">
        <span v-if="!toggle">Show Tasks</span>
        <span v-else>Hide Tasks List ({{ tasks.filter(x=>x.done).length }} of {{tasks.length}})</span>
      </b-button>
    </div>
    <h3>Tasks:</h3>
      <button @click="addTask">Add Laundry Task</button>
      <div v-if="toggle" class="taskView">
        <div :key="r.id" v-for="r in tasks">
          <div class="columns">
            <div class="column">
              <span>{{ r.desc }} </span>
            </div>
            <div class="column">
              <font-awesome-icon icon="spinner" :spin="!r.done" v-if="!r.done" />
              <font-awesome-icon style="color:green" icon="check" v-if="r.done" />
            </div>
          </div>
        </div>
      </div>
    <br />
    <br />
  </div>
</template>

<script>
import {HubConnectionBuilder} from '@microsoft/signalr'

const connection = new HubConnectionBuilder().withUrl("https://localhost:44321/notify").build();
connection.start();
export default {
  name: 'HelloWorld',
  props: {
    
  },
  data(){
    return {
      tasks:[],
      currentIds:1,
      toggle:false,
      
    }
  },
  mounted() {
    connection.on("AddTask",task=>{
      this.tasks.push(task)
    })

    // connection.on("TaskDone", ta)
  },
  methods:{
    addTask:function(){
      connection.invoke("AddTask" , {id:this.currentId , desc : "test" , done: false});
      this.currentId++
    }
  }
}
</script>

