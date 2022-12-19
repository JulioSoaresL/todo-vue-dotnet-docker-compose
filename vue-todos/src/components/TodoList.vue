<template>
    <el-row>
        <el-col :span="12" :offset="7" style="width: 100%">
            <h1>TodoList</h1>
            <todo-form @send-message="createTodo"/>
            <el-table :data="todos">
                <el-table-column prop="title" label="Tarefa" width="350"/>
                <el-table-column fixed="right" label="Opções" width="200">
                    <template #default="scope">
                        <el-space wrap>
                            <el-switch v-model="scope.row.completed" @click="updateTodo(scope.row)"/>
                            <el-popconfirm 
                                confirm-button-text="Sim" 
                                cancel-button-text="Não" 
                                icon="el-icon-info" icon-color="red" 
                                title="Apagar Tarefa?"
                                @confirm="handleDelete(scope.row)"
                                @cancel="cancelDelete">
                                <template #reference>
                                    <el-button type="danger">
                                        Delete
                                    </el-button>
                                </template>
                            </el-popconfirm>
                        </el-space>
                    </template>
                </el-table-column>
            </el-table>
        </el-col>
    </el-row>
</template>

<script lang="ts">

import { ElMessage } from 'element-plus';
import { Options, Vue } from 'vue-class-component';
import TodoForm from './TodoForm.vue';

interface Todo {
    id: number;
    title: string;
    completed: boolean;
}

@Options({
    components: {
        TodoForm,
    }
})

export default class TodoList extends Vue {
    todos = []

    async mounted() {
        await this.loadTodos();
    }

    async loadTodos() {
        const response = await this.axios.get(`https://localhost:7188/api/Todos`);
        this.todos = response.data
    }
    
    async createTodo(todo: any) {
    console.log("Todo", todo);
    await this.axios.post(`https://localhost:7188/api/Todos`, {
        title: todo.title,
        completed: todo.completed
    });
    ElMessage({
        message: "Tarefa Criada",
        type: "success"
    });
    await this.loadTodos();
   }

   async updateTodo(todo: Todo) {
        console.log("Todo", todo);
        await this.axios.put(`https://localhost:7188/api/Todos/${todo.id}`, {
            id: todo.id,
            title: todo.title,
            completed: todo.completed,
        });
        ElMessage({
            message: "Tarefa Atualizada!",
            type: "success"
        });
        await this.loadTodos();
   }

   async handleDelete(todo: Todo) {
        await this.axios.delete(`https://localhost:7188/api/Todos/${todo.id}`);
        ElMessage({
            message: "Deletada com Sucesso",
            type: "success"
        });
        await this.loadTodos();
   }

   cancelDelete() {
        console.log("O delete foi cancelado");
   }
}
</script>