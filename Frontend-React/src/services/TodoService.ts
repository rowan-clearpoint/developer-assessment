import axios from 'axios';
import {TodoItem} from '../models/todo/TodoItem';

const apiUrl = 'https://localhost:5001/api/todoitems';

export const addTodoItem = async (description: string): Promise<TodoItem> => {
  const response = await axios.post(apiUrl, { description });
  return response.data;
};
