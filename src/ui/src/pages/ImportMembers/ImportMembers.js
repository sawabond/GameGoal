import React, { useState } from 'react';
import axios from 'axios';
import Header from '../../components/Header';
import { Button } from '@material-ui/core';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
export default function ImportMembers() {
  const [selectedFile, setSelectedFile] = useState();
  const [isFilePicked, setIsFilePicked] = useState(false);

  const changeHandler = (event) => {
    setSelectedFile(event.target.files[0]);
    setIsFilePicked(true);
  };

  const handleSubmission = () => {
    const formData = new FormData();

    formData.append('memberList', selectedFile);
    axios
      .post('https://localhost:7184/api/User/import-members', formData, {
        headers: {
          Authorization: 'Bearer ' + StorageUser.token,
        },
      })
      .then((response) => {
        if (response.data.error) {
          console.log(response.data.error);
        }
      });
  };
  let StorageUser = JSON.parse(sessionStorage.getItem('user'));
  console.log(StorageUser);
  return (
    <>
      <Header />
      <ToastContainer />
      <input type="file" name="file" onChange={changeHandler} />
      {isFilePicked ? (
        <div>
          <p>Filename: {selectedFile.name}</p>
          <p>Filetype: {selectedFile.type}</p>
        </div>
      ) : (
        <p>Select a file to show details</p>
      )}
      <div>
        <Button color="primary" onClick={handleSubmission}>
          Submit
        </Button>
      </div>
    </>
  );
}
