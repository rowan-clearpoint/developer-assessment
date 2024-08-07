import React, { useState } from 'react';
import { Button, Container, Row, Col, Form, Stack, Alert } from 'react-bootstrap';
import { addTodoItem } from '../../services/TodoService';

const AddTodoItemForm = () => {
  const [description, setDescription] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  async function handleAdd() {
    try {
      await addTodoItem(description);
      setDescription('');
      setSuccess('Item added successfully!');
      setError(null); // Clear any existing error messages
    } catch (error: any) {
      if (error.response) {
        if (error.response.status === 500) {
          setError('Something went wrong.');
        } else if (error.response.data && error.response.data.errors) {
          // Handle different error structures
          const errorMessages = Array.isArray(error.response.data.errors)
            ? error.response.data.errors.join(' ')
            : Object.values(error.response.data.errors).flat().join(', ');
          setError(errorMessages || 'Something went wrong. Please try again.');
        } else {
          setError('Failed to add item');
        }
      } else {
        setError('Failed to add item');
      }
      setSuccess(null); // Clear any existing success messages
      console.error(error);
    }
  }

  function handleClear() {
    setDescription('');
    setError(null); // Clear error messages when clearing input
    setSuccess(null); // Clear success messages when clearing input
  }

  const handleDescriptionChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setDescription(event.target.value);
  };

  return (
    <Container>
      <h1>Add Item</h1>
      {success && <Alert variant="success">{success}</Alert>}
      {error && <Alert variant="danger">{error}</Alert>}
      <Form.Group as={Row} className="mb-3" controlId="formAddTodoItem">
        <Form.Label column sm="2">
          Description
        </Form.Label>
        <Col md="6">
          <Form.Control
            type="text"
            placeholder="Enter description..."
            value={description}
            onChange={handleDescriptionChange}
          />
        </Col>
      </Form.Group>
      <Form.Group as={Row} className="mb-3 offset-md-2" controlId="formAddTodoItem">
        <Stack direction="horizontal" gap={2}>
          <Button variant="primary" onClick={handleAdd}>
            Add Item
          </Button>
          <Button variant="secondary" onClick={handleClear}>
            Clear
          </Button>
        </Stack>
      </Form.Group>
    </Container>
  );
};

export default AddTodoItemForm;
