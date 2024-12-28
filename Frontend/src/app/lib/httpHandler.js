let BE_ENPOINT = "https://localhost:7017";

const HEADERS = {
  "Content-Type": "application/json",
  accept: "application/json",
};
const fetchGet = async (uri, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "GET",
      headers: HEADERS,
    });
    const data = await res.json();
    if (!res.ok) {
      onFail(data);
      return;
    }
    onSuccess(data);
  } catch {
    onException();
  }
};

const fetchPost = async (uri, reqData, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "POST",
      headers: HEADERS,
      body: JSON.stringify(reqData),
    });
    const data = await res.json();
    if (!res.ok) {
      onFail(data);
      return;
    }
    onSuccess(data);
  } catch (e) {
    console.log(e);
    onException();
  }
};

const fetchDelete = async (uri, reqData, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "DELETE",
      headers: HEADERS,
      body: JSON.stringify(reqData),
    });
    const data = await res.json();
    if (!res.ok) {
      onFail(data);
      return;
    }
    onSuccess(data);
  } catch {
    onException();
  }
};

const fetchPut = async (uri, reqData, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "PUT",
      headers: HEADERS,
      body: JSON.stringify(reqData),
    });
    const data = await res.json();
    if (!res.ok) {
      onFail(data);
      return;
    }
    onSuccess(data);
  } catch {
    onException();
  }
};

export { fetchGet, fetchPost, fetchDelete, fetchPut };
