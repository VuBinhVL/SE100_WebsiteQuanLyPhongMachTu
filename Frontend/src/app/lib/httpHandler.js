let BE_ENPOINT = "https://localhost:7017";

const HEADERS = {
  "Content-Type": "application/json",
  accept: "application/json",
};

const getHeaders = () => {
  const token = localStorage.getItem("jwtToken");
  if (token === null) {
    return HEADERS;
  }
  return {
    ...HEADERS,
    Authorization: `Bearer ${token}`,
  };
};
const fetchGet = async (uri, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "GET",
      headers: getHeaders(),
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
      headers: getHeaders(),
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
      headers: getHeaders(),
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
      headers: getHeaders(),
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
const fetchUpload = async (uri, formData, onSuccess, onFail, onException) => {
  try {
    const res = await fetch(BE_ENPOINT + uri, {
      method: "POST",
      body: formData,
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
export { fetchGet, fetchPost, fetchDelete, fetchPut, fetchUpload, BE_ENPOINT };
