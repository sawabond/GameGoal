import React, { useState } from 'react';
import axios from 'axios';
import Header from '../../components/Header';
import { Button } from '@material-ui/core';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './ImportMembers.scss';
import FileUploadIcon from '@mui/icons-material/FileUpload';
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

      <div
        className="upload"
        style={{
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          height: '100%',
        }}
      >
        <div
          className="upload-choose-file"
          style={{
            width: '25%',
          }}
        >
          <label className="custom-file-upload">
            <input type="file" multiple onChange={changeHandler} />
            <div
              className="div"
              style={{ display: 'flex', alignItems: 'center' }}
            >
              <FileUploadIcon />
              {isFilePicked ? <p>{selectedFile.name}</p> : <p>Choose File</p>}
            </div>
          </label>
        </div>
        <div className="upload-button">
          <Button color="primary" onClick={handleSubmission}>
            Upload
          </Button>
        </div>
      </div>
    </>
  );
}
