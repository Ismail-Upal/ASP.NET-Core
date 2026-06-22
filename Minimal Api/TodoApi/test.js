
import http from 'k6/http';
import { check } from 'k6';

export const options = {
  // We use 'stages' to slowly increase the pressure
  stages: [
    { duration: '1m', target: 100 },  // Minute 1: Ramp up to 100 users
    { duration: '1m', target: 100 },  // Minute 2: Stay at 100 users
    { duration: '1m', target: 500 },  // Minute 3: Ramp up to 500 users
    { duration: '1m', target: 500 },  // Minute 4: Stay at 500 users
    { duration: '1m', target: 1000 }, // Minute 5: Ramp up to 1000 users
    { duration: '1m', target: 1000 }, // Minute 6: Stay at 1000 users
    { duration: '30s', target: 0 },   // Minute 7: Cool down to 0
  ],
};

export default function () {
  const baseUrl = 'http://localhost:5098/api/todoitems';

  // 1. GET ALL
  let res = http.get(baseUrl);
  check(res, { '1. GET ALL returned 200': (r) => r.status === 200 });

  // 2. POST (Create)
  const payload = JSON.stringify({ name: 'Max Test Task', isComplete: false });
  const headers = { 'Content-Type': 'application/json' };
  res = http.post(baseUrl, payload, { headers });
  check(res, { '2. POST created (201)': (r) => r.status === 201 });

  const newId = res.json().id;

  // 3. GET BY ID
  res = http.get(`${baseUrl}/${newId}`);
  check(res, { '3. GET BY ID (200)': (r) => r.status === 200 });

  // 4. PUT (Update)
  const updatePayload = JSON.stringify({ id: newId, name: 'Updated', isComplete: true });
  res = http.put(`${baseUrl}/${newId}`, updatePayload, { headers });
  check(res, { '4. PUT updated (204)': (r) => r.status === 204 });

  // 5. DELETE
  res = http.del(`${baseUrl}/${newId}`);
  check(res, { '5. DELETE removed (204)': (r) => r.status === 204 });
}
